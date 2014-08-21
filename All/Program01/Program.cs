namespace Program01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HyLibrary.ExtensionMethod;
    using HyLibrary.Reflection;
    using HyLibrary.Lambda;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Data.Entity.SqlServer;

    public class Tree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Test { get; set; }
        public string HierarchyId { get; set; }
    }

    public static class Functions
    {
        /// <summary>
        /// 准备一个标量函数， 判断是否是父级的后代（只检查一级）
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool IsChild(string hierarchy, string match)
        {
            return hierarchy.StartsWith(match) && hierarchy != match;
        }

        /// <summary>
        /// 准备一个聚集函数， 获取最大HierarchyId
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        public static string MaxHierarchy(string hierarchy)
        {
            return hierarchy;
        }
    }

    public class EFRepository 
    {
        private List<Tree> tables;

        public EFRepository()
        {
            this.tables = new List<Tree>() 
            {
                new Tree { Id = 1, HierarchyId = "/" },
                new Tree { Id = 2, HierarchyId = "/1/" },
                new Tree { Id = 3, HierarchyId = "/2/" },
                new Tree { Id = 4, HierarchyId = "/1/1/" },
                new Tree { Id = 5, HierarchyId = "/1/2/" },
            };
        }

        /// <summary>
        /// 原始查询语句
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        public string GetMaxChildOrigin(string hierarchy)
        {
            return this.tables.Where(k => Functions.IsChild(k.HierarchyId, hierarchy))
                       .Select(k => Functions.MaxHierarchy(k.HierarchyId))
                       .ToList().Join(",");
        }

        public string GetMaxChild(string propName, string hierarchy)
        {
            //Expression<Func<Tree, bool>> whereExpOrigin = (Tree k) => Functions.IsChild(k.HierarchyId, hierarchy);
            //Expression<Func<Tree, string>> selectExpOrigin = (Tree k) => Functions.MaxHierarchy(k.HierarchyId);

            var param = Expression.Parameter(typeof(Tree), "k");
            var prop = Expression.Property(param, propName);

            var ischild = Expression.Call(
                typeof(Functions).GetMethod("IsChild"),
                prop,
                Expression.Constant(hierarchy, typeof(string)));
            var whereExp = Expression.Lambda<Func<Tree, bool>>(
                ischild,
                param);

            var max = Expression.Call(
                typeof(Functions).GetMethod("MaxHierarchy"),
                prop);
            var selectExp = Expression.Lambda<Func<Tree, string>>(max, param);

            // 测试
            return this.tables.Where(whereExp.Compile())
                       .Select(selectExp.Compile())
                       .ToList().Join(",");

            // 真实查询语句
            //return this.tables.Where(whereExp.Compile())
            //           .Select(selectExp.Compile())
            //           .FirstOrDefault();
        }

        public string GetMaxChild(Expression<Func<Tree, string>> propertyExp, string hierarchy)
        {
            var param = propertyExp.Parameters[0];
            var prop = propertyExp.Body;

            var ischild = Expression.Call(
                typeof(Functions).GetMethod("IsChild"),
                prop,
                Expression.Constant(hierarchy, typeof(string)));
            var whereExp = Expression.Lambda<Func<Tree, bool>>(
                ischild,
                param);
            
            return this.tables.Where(whereExp.Compile())
                       .Select(propertyExp.Compile())
                       .ToList().Join(",");

            // 真实查询语句
            //return this.tables.Where(whereExp.Compile())
            //           .Select(propertyExp.Compile())
            //           .FirstOrDefault();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var repository = new EFRepository();

            Console.WriteLine(repository.GetMaxChildOrigin("/1/"));

            Console.WriteLine(repository.GetMaxChild("HierarchyId", "/1/"));

            Console.WriteLine(repository.GetMaxChild(k => Functions.MaxHierarchy(k.HierarchyId), "/1/"));
        }
    }
}
