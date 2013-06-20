using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public  interface IUserProfileService
    {
       bool ChangePassword(int profileId, string password);
       UserProfile GetUser(string userName);
       bool EditInfo(BLL.UserProfile profile);
       bool DeleteByID(int id);
       UserProfile FindById(int id);

    }
}
