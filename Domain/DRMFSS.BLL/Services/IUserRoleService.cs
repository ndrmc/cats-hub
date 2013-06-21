using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IUserRoleService
    {
       bool AddUserRole(UserRole entity);
       bool DeleteUserRole(UserRole entity);
       bool DeleteById(int id);
       bool EditUserRole(UserRole entity);
       UserRole FindById(int id);
       List<UserRole> GetAllUserRole();
       List<UserRole> FindBy(Expression<Func<UserRole, bool>> predicate);

       
        

    }
}


     


      
