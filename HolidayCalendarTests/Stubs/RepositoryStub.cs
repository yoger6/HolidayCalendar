using System;
using System.Collections.Generic;
using System.Linq;
using DataLib.Repositories;

namespace HolidayCalendarTests.Stubs
{
    public class RepositoryStub<T> : Repository<T> where T : class
    {

        protected readonly List<T> Items; 

        public event EventHandler DisposeCalled;
        public event EventHandler GetCalled;
        public event EventHandler AddCalled;
        public event EventHandler UpdateCalled;
        public event EventHandler SaveCalled;


        public RepositoryStub(List<T> items)
        {
            Items = items;
        } 


        public override List<T> GetAll()
        {
            OnGetCalled();
            return Items;
        }

        public override void Add(T entity)
        {
            Items.Add(entity);
            OnAddCalled();
        }

        public override void Update(T entity)
        {
            var listItem = Items.FirstOrDefault(x => x.Equals(entity));
            var itemsIndex = Items.IndexOf(listItem);

            Items[itemsIndex] = entity;
            OnUpdateCalled();
        }

        public override void Save()
        {
            OnSaveCalled();
        }

        protected override void Dispose(bool disposing)
        {
            OnDisposeCalled();
        }

        protected virtual void OnGetCalled()
        {
            GetCalled?.Invoke(this, EventArgs.Empty);
        }
        
        protected virtual void OnDisposeCalled()
        {
            DisposeCalled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAddCalled()
        {
            AddCalled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpdateCalled()
        {
            UpdateCalled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSaveCalled()
        {
            SaveCalled?.Invoke(this, EventArgs.Empty);
        }
    }
}