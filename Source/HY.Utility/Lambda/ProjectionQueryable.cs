namespace HY.Utitily.Lambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectionQueryable<TSource>
    {
        private readonly IQueryable<TSource> source;

        public ProjectionQueryable(IQueryable<TSource> source)
        {
            this.source = source;
        }

        public IQueryable<TDest> To<TDest>()
        {
            return this.source.Select(ProjectionExpression<TSource, TDest>.AsExpression());
        }
    }
}