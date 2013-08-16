using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IUserHubService:IDisposable
    {
       bool AddUserHub(UserHub entity);
        void AddUserHub(int warehouseID, int userID);
       bool DeleteUserHub(UserHub entity);
       bool DeleteById(int id);
       bool EditUserHub(UserHub entity);
       UserHub FindById(int id);
       List<UserHub> GetAllUserHub();
       List<UserHub> FindBy(Expression<Func<UserHub, bool>> predicate);
       
    }
}

   


