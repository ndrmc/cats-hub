using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public  interface IUserProfileService
    {

       bool AddUserProfile(UserProfile entity);
       bool DeleteUserProfile(UserProfile entity);
       bool DeleteById(int id);
       bool EditUserProfile(UserProfile entity);
       UserProfile FindById(int id);
       List<UserProfile> GetAllUserProfile();
       List<UserProfile> FindBy(Expression<Func<UserProfile, bool>> predicate);

       bool ChangePassword(int profileId, string password);
       UserProfile GetUser(string userName);
       bool EditInfo(BLL.UserProfile profile);
      

    }
}


     


      
    