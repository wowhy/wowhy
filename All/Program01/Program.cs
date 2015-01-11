namespace Program01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using HyLibrary.Lambda;

    public enum HSDataSortDirection
    {
        /// <summary>
        /// 缺省
        /// </summary>
        Default = 0,

        /// <summary>
        /// 升序
        /// </summary>
        ASC = 1,

        /// <summary>
        /// 降序
        /// </summary>
        DESC = 2
    }

    public class HSDataSorter
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public HSDataSorter()
        {
        }

        /// <summary>
        /// 排序方式。
        /// </summary>
        public HSDataSortDirection Direction { get; set; }

        /// <summary>
        /// 排序属性。
        /// </summary>
        public string Property { get; set; }
    }

    public class Test
    {
        public int Id { get; set; }

        public string Name { get; set; } 
    }

    public class TestDbContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        public TestDbContext()
            : base("name=test")
        {
        }
    }

    public static class Helper
    {
        public static IQueryable<TSource> HSOrderBy<TSource>(
            this IQueryable<TSource> source,
            params HSDataSorter[] sorters)
        {
            var query = source;
            if (sorters != null && sorters.Length > 0)
            {
                var param = Expression.Parameter(typeof(TSource), "ob");
                var queryableType = typeof(Queryable);
                var firstOrder = true;

                for (var i = 0; i < sorters.Length; i++)
                {
                    var sorter = sorters[i];
                    if (sorter.Direction == HSDataSortDirection.Default ||
                        string.IsNullOrEmpty(sorter.Property))
                    {
                        continue;
                    }

                    var memExpr = Expression.Property(param, sorter.Property);
                    var types = new Type[2] { typeof(TSource), memExpr.Type };
                    string sortingDir;

                    if (firstOrder)
                    {
                        firstOrder = false;
                        sortingDir = sorter.Direction == HSDataSortDirection.ASC ? "OrderBy" : "OrderByDescending";
                    }
                    else
                    {
                        sortingDir = sorter.Direction == HSDataSortDirection.ASC ? "ThenBy" : "ThenByDescending";
                    }

                    var expr = Expression.Call(
                        typeof(Queryable),
                        sortingDir,
                        types,
                        query.Expression,
                        Expression.Lambda(memExpr, param));

                    query = query.Provider.CreateQuery<TSource>(expr);
                }
            }

            return query;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new TestDbContext())
            //{
            //    context.Tests.AsQueryable().HSOrderBy(
            //        new HSDataSorter { Property = "Id", Direction = HSDataSortDirection.ASC },
            //        new HSDataSorter { Property = "Name", Direction = HSDataSortDirection.DESC })
            //           .ToList();
            //}
        }
    }
}