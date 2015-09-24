using System;
using System.Collections.Generic;

namespace DataLib.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}