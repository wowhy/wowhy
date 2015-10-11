namespace HyLibrary.Lambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectionEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> source;

        public ProjectionEnumerable(IEnumerable<TSource> source)
        {
            this.source = source;
        }

        public IEnumerable<TDest> To<TDest>()
        {
            return this.source.Select(ProjectionExpression<TSource, TDest>.AsFunc());
        }
    }
}