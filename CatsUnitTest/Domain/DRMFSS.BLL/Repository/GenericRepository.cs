using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        /// <summary>
        /// THis method canb used to get objects with their object graph included
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"> properties separeted by comma</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _entities.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }
    }
}
