
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IRoleService
    {

        bool AddRole(RoleService entity);
        bool DeleteRole(RoleService entity);
        bool DeleteById(int id);
        bool EditRole(RoleService entity);
        RoleService FindById(int id);
        List<RoleService> GetAllRole();
        List<RoleService> FindBy(Expression<Func<RoleService, bool>> predicate);


    }
}


      
