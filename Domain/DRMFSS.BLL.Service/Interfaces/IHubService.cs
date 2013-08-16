using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IHubService:IDisposable
    {
        bool AddHub(Hub hub);
        bool DeleteHub(Hub hub);
        bool DeleteById(int id);
        bool EditHub(Hub hub);
        Hub FindById(int id);
        List<Hub> GetAllHub();
        List<Hub> FindBy(Expression<Func<Hub, bool>> predicate);
        List<ViewModels.Common.StoreViewModel> GetAllStoreByUser(UserProfile user);
        IEnumerable<StockStatusReport> GetStockStatusReport(int hubID, int commodityID);
        IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID);
        IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID);
    }
}
