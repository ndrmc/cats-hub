using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public abstract class GenericRepository<C, T> :
    IGenericRepository<T>
        where T : class
        where C : DbContext//, new()
    {

        private C _entities;// = new C();
        public C db
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public IUnitOfWork repository { get; set; }

        public virtual List<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query.ToList();
        }

        public List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query.ToList();
        }
        public virtual void Attach(T entity)
        {
            _entities.Set<T>().Attach(entity);
        }
        
        public virtual bool Add(T entity)
        {
            _entities.Set<T>().Add(entity);
            return true;
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual bool SaveChanges(T entity)
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Set<T>().Attach(entity);
            }
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.SaveChanges();
            return true;
        }

    }
}
