using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report.Data;
using System.Linq.Expressions;
namespace DRMFSS.BLL.Services
{
   public  interface IStoreService
    {

        bool AddStore(Store store);
        bool DeleteStore(Store store);
        bool DeleteById(int id);
        bool EditStore(Store store);
        Store FindById(int id);
        List<Store> GetAllStore();
        List<Store> FindBy(Expression<Func<Store, bool>> predicate);

       List<Store> GetStoreByHub(int hubId);
       List<int> GetStacksByStoreId(int? StoreId);
       List<Store> GetStoresWithBalanceOfCommodityAndSINumber(int parentCommodityId, int SINumber, int hubId);
       List<Store> GetStoresWithBalanceOfCommodity(int parentCommodityId, int hubId);
       List<int> GetStacksWithSIBalance(int storeId, int siNumber);
       List<int> GetStacksByToStoreIdFromStoreIdFromStack(int ToStoreId, int FromStoreId, int FromStackId);
       List<Store> GetAllByHUbs(List<Hub> HubIds);
       IEnumerable<BinCardViewModel> GetBinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID);
       List<Store> GetStoreByHub(int hubId);
    }
}


      


          
      