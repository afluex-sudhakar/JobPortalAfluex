using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

        TEntity GetById(int id);

        TEntity GetByName(string name);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Func<TEntity, bool> clause);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = 0,
           int? pageSize = null,
           params SortExpression<TEntity>[] sortExpressions);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Dispose();
    }
}
