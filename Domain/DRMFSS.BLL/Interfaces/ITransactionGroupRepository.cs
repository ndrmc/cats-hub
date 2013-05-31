using System;
namespace DRMFSS.BLL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionGroupRepository :  IGenericRepository<TransactionGroup>,IRepository<TransactionGroup>
    {
        /// <summary>
        /// Gets the last trasaction group id.
        /// </summary>
        /// <returns></returns>
       Guid GetLastTrasactionGroupId();
        
    }
}
