using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Common;
using DRMFSS.BLL.ViewModels.Report.Data;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IHubRepository : IGenericRepository<Hub>,IRepository<Hub>
    {
        List<StoreViewModel> GetAllStoreByUser(UserProfile user);

        List<Hub> GetAllWithoutId(int p);

        List<Hub> GetOthersHavingSameOwner(Hub hub);

        List<Hub> GetOthersWithDifferentOwner(Hub hub);

        IEnumerable<StockStatusReport> GetStockStatusReport(int hubID, int commodityID);

        IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID);

        IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID);

        List<FreeStockProgram> GetFreeStockGroupedByProgram(int HuBID, FreeStockFilterViewModel freeStockFilterViewModel);
    }
}
