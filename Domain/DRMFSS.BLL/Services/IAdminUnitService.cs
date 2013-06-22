
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IAdminUnitService
    {

        bool AddAdminUnit(AdminUnit entity);
        bool DeleteAdminUnit(AdminUnit entity);
        bool DeleteById(int id);
        bool EditAdminUnit(AdminUnit entity);
        AdminUnit FindById(int id);
        List<AdminUnit> GetAllAdminUnit();
        List<AdminUnit> FindBy(Expression<Func<AdminUnit, bool>> predicate);


    }
}


