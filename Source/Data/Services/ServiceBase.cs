using Data.Interfaces.Repositories;
using Data.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this.repository = repository;
        }

        public void Add(TEntity entity)
        {
            repository.Add(entity);
        }

        public TEntity GetById(int id)
        {
            return repository.GetById(id);
        }

        public TEntity GetByName(string name)
        {
            return repository.GetByName(name);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            repository.Remove(entity);
        }

        public void Dispose()
        {
            repository.Dispose();
        }
    }
}
