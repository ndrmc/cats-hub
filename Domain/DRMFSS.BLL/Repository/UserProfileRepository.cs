using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserProfileRepository : IUserProfileRepository
    {
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="profileId">The profile id.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool ChangePassword(int profileId, string password)
        {
            BLL.UserProfile user = db.UserProfiles.
                Where(p => p.UserProfileID == profileId).SingleOrDefault();
            if (user != null)
            {
                user.Password = password;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public BLL.UserProfile GetUser(string userName)
        {
            
            return (from u in db.UserProfiles
                    where u.UserName == userName && !u.LockedInInd && u.ActiveInd
                    select u).FirstOrDefault();
        }

        /// <summary>
        /// Edits the info.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        public bool EditInfo(BLL.UserProfile profile)
        {
            BLL.UserProfile user = db.UserProfiles.
                Where(p => p.UserProfileID == profile.UserProfileID).SingleOrDefault();
            if (user != null)
            {
                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.GrandFatherName = profile.GrandFatherName;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        
    }
}
