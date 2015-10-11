namespace HY.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HY.Domain.Entity;

    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        IQueryable<TEntity> Query { get; }

        TEntity GetById(TKey id);

        void Add(TEntity entity);

        void Edit(TEntity entity);

        void Remove(TEntity entity);

        void RemoveById(TKey id);
    }
}