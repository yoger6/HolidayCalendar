using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLib.Contexts;

namespace DataLib.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class 
    {
        private readonly HolidyCalendarContext _context;
        protected DbSet<T> DbSet { get; set; }

        protected Repository()
        : this ("DefaultConnection")
        { }

        protected Repository(string connectionString)
        {
            _context = new HolidyCalendarContext(connectionString);
            DbSet = _context.Set<T>();
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
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
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
               _context.Dispose();
            }
        }
    }
}
