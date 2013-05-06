using System;
namespace DRMFSS.BLL.Interfaces

{
    /// <summary>
    /// Account Repository
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
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
