using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Repository;

namespace DRMFSS.BLL.Interfaces
{


    public interface IAccountRepository :
          IGenericRepository<Account>,IRepository<Account>
    {
        /// <summary>
        /// Gets the account ID with create.
        /// </summary>
        /// <param name="EntityType">Type of the entity.</param>
        /// <param name="EntityID">The entity ID.</param>
        /// <returns></returns>
        int GetAccountIDWithCreate(string EntityType, int EntityID);
        /// <summary>
        /// Gets the account ID.
        /// </summary>
        /// <param name="EntityType">Type of the entity.</param>
        /// <param name="EntityID">The entity ID.</param>
        /// <returns></returns>
        int GetAccountID(string EntityType, int EntityID);

    }
         
      
         
      
}
