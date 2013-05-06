using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Common;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Report.Data;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HubRepository : IHubRepository
    {

        public List<ViewModels.Common.StoreViewModel> GetAllStoreByUser(UserProfile user)
        {
            if(user == null || user.DefaultHub == null)
            {
                return new List<StoreViewModel>();
            }
            //var stores = (from c in user.DefaultHub.Stores select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            var stores = (from c in repository.Store.GetStoreByHub(user.DefaultHub.HubID) select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            //stores.Insert(0, new ViewModels.Common.StoreViewModel { StoreName = "Total Hub" });  //I need it for report only so I will modify it on report
            return stores;
        }


        public List<Hub> GetAllWithoutId(int hubId)
        {
            return db.Hubs.Where(p => p.HubID != hubId).ToList();
        }


        public List<Hub> GetOthersHavingSameOwner(Hub hub)
        {
            return (from v in db.Hubs
                        where v.HubID != hub.HubID && v.HubOwnerID == hub.HubOwnerID
                        select v).ToList();
        }


        public List<Hub> GetOthersWithDifferentOwner(Hub hub)
        {
            return (from v in db.Hubs
                    where v.HubOwnerID != hub.HubOwnerID
                    select v).ToList();
        }

        public IEnumerable<StockStatusReport> GetStockStatusReport(int hubID, int commodityID)
        {
           var commodity = repository.Commodity.FindById(commodityID);
           if(commodity!=null && commodity.CommodityTypeID == 1)
            return db.RPT_StockStatus(hubID, commodityID);
           else
               return db.RPT_StockStatusNonFood(hubID, commodityID);
        }

        public IEnumerable<StatusReportBySI_Result> GetStatusReportBySI(int hubID)
        {
            return db.GetStatusReportBySI(hubID).AsEnumerable();
        }

        public IEnumerable<DispatchFulfillmentStatus_Result> GetDispatchFulfillmentStatus(int hubID)
        {
            return db.GetDispatchFulfillmentStatus(hubID);
        }


        public List<FreeStockProgram> GetFreeStockGroupedByProgram(int HuBID, FreeStockFilterViewModel freeStockFilterViewModel)
        {
           var dbGetStatusReportBySI = db.GetStatusReportBySI(HuBID).ToList();
           if (freeStockFilterViewModel.ProgramId.HasValue && freeStockFilterViewModel.ProgramId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ProgramID == freeStockFilterViewModel.ProgramId).ToList();
            }
           
            if (freeStockFilterViewModel.CommodityId.HasValue && freeStockFilterViewModel.CommodityId != 0)
           {
               dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.CommodityID == freeStockFilterViewModel.CommodityId).ToList();
           }

            if (freeStockFilterViewModel.ShippingInstructionId.HasValue && freeStockFilterViewModel.ShippingInstructionId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ShippingInstructionID == freeStockFilterViewModel.ShippingInstructionId).ToList();
            }
            if (freeStockFilterViewModel.ProjectCodeId.HasValue && freeStockFilterViewModel.ProjectCodeId != 0)
            {
                dbGetStatusReportBySI = dbGetStatusReportBySI.Where(p => p.ProjectCodeID == freeStockFilterViewModel.ProjectCodeId).ToList();
            }

            return (from t in dbGetStatusReportBySI
                    group t by new {t.ProgramID, t.ProgramName}
                    into b
                    select new FreeStockProgram()
                               {
                                   Name = b.Key.ProgramName,
                                   Details = b.Select(t1 => new FreeStockStatusRow()
                                                                                 {
                                                                                     SINumber = t1.SINumber,
                                                                                     Vessel = t1.Vessel,
                                                                                     Allocation = t1.AllocatedToHub ?? 0,
                                                                                     Dispatched =
                                                                                         t1.dispatchedBalance ?? 0,
                                                                                     Transported =
                                                                                         t1.fullyCommitedBalance ?? 0,
                                                                                     Donor = t1.Donor,
                                                                                     FreeStock = t1.UncommitedStock ?? 0,
                                                                                     PhysicalStock =
                                                                                         t1.TotalStockOnHand ?? 0,
                                                                                     Product = t1.CommodityName,
                                                                                     Program = t1.ProgramName,
                                                                                     Project = t1.ProjectCode,
                                                                                     ReceivedAmount =
                                                                                         t1.ReceivedBalance ?? 0,
                                                                                     Remark = ""
                                                                                 }).OrderBy(c=>c.Product).ToList()
                                                  }).ToList();

        }
    }
}
