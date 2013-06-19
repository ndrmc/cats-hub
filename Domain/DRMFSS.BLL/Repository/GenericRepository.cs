using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public class GenericRepository<T> :
        IGenericRepository<T>
        where T : class
    {

        private CTSContext _entities;

        public CTSContext db
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public GenericRepository(CTSContext context)
        {
            _entities = context;
        }

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


        public virtual bool Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            return true;
        }

        public virtual bool Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual T FindById(int id)
        {
            return _entities.Set<T>().Find(id);
        }
    }
}
