using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report.Data;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IStoreRepository : IGenericRepository<Store>,IRepository<Store>
    {
        /// <summary>
        /// Gets the store by hub.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        List<Store> GetStoreByHub(int hubId);

        /// <summary>
        /// Gets the stacks by store id.
        /// </summary>
        /// <param name="StoreId">The store id.</param>
        /// <returns></returns>
        List<int> GetStacksByStoreId(int? StoreId);

        /// <summary>
        /// Gets the stores with balance of commodity.
        /// </summary>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        List<Store> GetStoresWithBalanceOfCommodity(int parentCommodityId, int hubId);


        List<int> GetStacksWithSIBalance(int storeId, int siNumber);

        List<Store> GetStoresWithBalanceOfCommodityAndSINumber(int parentCommodityId, int SINumber, int hubId);

        List<int> GetStacksByToStoreIdFromStoreIdFromStack(int ToStoreId, int FromStoreId, int FromStackId);

        List<Store> GetAllByHUbs(List<Hub> list);

        IEnumerable<BinCardViewModel> GetBinCard(int UserProfileID, int? StoreID, int? CommodityID, string ProjectID);
    }
}
