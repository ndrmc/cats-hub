using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IUserProfileRepository : IGenericRepository<UserProfile>,IRepository<UserProfile>
    {
       
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="profileId">The profile id.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool ChangePassword(int profileId, string password);
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        BLL.UserProfile GetUser(string userName);
        /// <summary>
        /// Edits the info.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        bool EditInfo(BLL.UserProfile profile);
    }
}
