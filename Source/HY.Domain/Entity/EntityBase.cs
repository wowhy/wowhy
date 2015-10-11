namespace HY.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}