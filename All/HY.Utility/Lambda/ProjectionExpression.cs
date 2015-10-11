namespace HyLibrary.Lambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectionExpression<TSource, TDest>
    {
        private static object state = new object();

        private static Expression<Func<TSource, TDest>> expression;

        private static Func<TSource, TDest> func;

        public static IQueryable<TDest> To(IQueryable<TSource> source)
        {
            return source.Select(AsExpression());
        }

        public static IEnumerable<TDest> To(IEnumerable<TSource> source)
        {
            CodeCheck.NotNull(source, "source");
            return source.Select(AsFunc());
        }

        public static Expression<Func<TSource, TDest>> AsExpression()
        {
            if (expression == null)
            {
                lock (state)
                {
                    if (expression == null)
                    {
                        expression = BuildExpression();
                    }
                }
            }

            return expression;
        }

        public static Func<TSource, TDest> AsFunc()
        {
            if (func == null)
            {
                func = AsExpression().Compile();
            }

            return func;
        }

        private static Expression<Func<TSource, TDest>> BuildExpression()
        {
            var sourceType = typeof(TSource);
            var destType = typeof(TDest);

            var sourceMembers = sourceType.GetProperties().Where(k => k.CanWrite)
                                          .ToDictionary(k => k.Name);

            var parameter = Expression.Parameter(sourceType, "k");

            var bindings = destType.GetProperties().Where(k => k.CanWrite)
                                   .Select(k =>
                                   {
                                       return BuildBinding(sourceMembers, k, parameter);
                                   })
                                   .Where(k => k != null);

            return Expression.Lambda<Func<TSource, TDest>>(
                Expression.MemberInit(
                    Expression.New(destType),
                    bindings),
                parameter);
        }

        private static MemberAssignment BuildBinding(
            Dictionary<string, PropertyInfo> sourceMembers,
            PropertyInfo property,
            ParameterExpression parameter)
        {
            if (sourceMembers.ContainsKey(property.Name))
            {
                return Expression.Bind(property, Expression.Property(parameter, sourceMembers[property.Name]));
            }

            var propertyNames = property.Name.Split('_').Where(k => !string.IsNullOrWhiteSpace(k))
                                        .ToArray();

            if (propertyNames.Length >= 2 && propertyNames.Length <= 16)
            {
                return BuildChildBinding(sourceMembers, property, propertyNames, parameter);
            }

            return null;
        }

        private static MemberAssignment BuildChildBinding(
            Dictionary<string, PropertyInfo> sourceMembers,
            PropertyInfo property,
            string[] propertyNames,
            ParameterExpression parameter)
        {
            var sourceProperty = sourceMembers.Where(k => k.Value.Name == propertyNames[0])
                                              .Select(k => k.Value)
                                              .FirstOrDefault();

            if (sourceProperty == null)
            {
                return null;
            }

            var propertyExpression = Expression.Property(parameter, sourceProperty);

            for (var i = 1; i < propertyNames.Length; i++)
            {
                sourceProperty = sourceProperty.PropertyType.GetProperties()
                                               .Where(k => k.Name == propertyNames[i])
                                               .FirstOrDefault();

                if (sourceProperty == null)
                {
                    return null;
                }

                propertyExpression = Expression.Property(propertyExpression, sourceProperty);
            }

            return Expression.Bind(property, propertyExpression);
        }
    }
}