using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
        bool SaveChanges(T entity);
      
    }
}
