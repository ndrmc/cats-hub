using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IUserHubService
    {
       bool AddUserHub(UserHub entity);
       bool DeleteUserHub(UserHub entity);
       bool DeleteById(int id);
       bool EditUserHub(UserHub entity);
       UserHub FindById(int id);
       List<UserHub> GetAllUserHub();
       List<UserHub> FindBy(Expression<Func<UserHub, bool>> predicate);
       
    }
}

   


