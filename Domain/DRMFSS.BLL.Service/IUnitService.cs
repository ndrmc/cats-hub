using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public interface IUnitService:IDisposable
    {
        bool AddUnit(Unit unit);
        bool DeleteUnit(Unit unit);
        bool DeleteById(int id);
        bool EditUnit(Unit unit);
        Unit FindById(int id);
        List<Unit> GetAllUnit();
        List<Unit> FindBy(Expression<Func<Unit, bool>> predicate);
      
    }
}


   
       




      
