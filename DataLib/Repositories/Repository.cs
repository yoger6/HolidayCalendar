using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLib.Contexts;

namespace DataLib.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class 
    {
        protected readonly HolidayCalendarContext Context;
        protected DbSet<T> DbSet { get; set; }

        protected Repository()
        : this ("DefaultConnection")
        { }

        protected Repository(string connectionString)
        {
            Context = new HolidayCalendarContext(connectionString);
            DbSet = Context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual void Add(T entity)
        {

            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               Context.Dispose();
            }
        }
    }
}
