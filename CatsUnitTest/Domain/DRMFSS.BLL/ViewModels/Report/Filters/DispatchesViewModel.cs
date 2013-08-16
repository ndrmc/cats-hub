using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Repository;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.BLL.ViewModels.Report
{
    /// <summary>
    /// view for Dispatches view and Wrapping  filtering criteria objects
    /// </summary>
    public class DispatchesViewModel
    {
        public List<BidRefViewModel> BidRefs { get; set; }
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
        public List<StoreViewModel> Stores { get; set; }
        public List<AreaViewModel> Areas { get; set; }


        public string bidRefId { set; get; }
        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? StoreId { get; set; }
        public int? AreaId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DispatchesViewModel()
        {
        }

        public DispatchesViewModel(IUnitOfWork Repository, UserProfile user)
        {
            this.Periods = GetAllPeriod();
            this.Cods = ConstantsRepository.GetAllCodes();
            this.CommodityTypes = Repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = Repository.Program.GetAllProgramsForReport();
            this.Stores = Repository.Hub.GetAllStoreByUser(user);
            this.Areas = Repository.AdminUnit.GetAllAreasForReport();
            this.BidRefs = Repository.DispatchAllocation.GetAllBidRefsForReport();
        }

        public List<PeriodViewModel> GetAllPeriod()
        {
            List<PeriodViewModel> periodes = new List<PeriodViewModel>();
            periodes.Add(new PeriodViewModel { PeriodId = 6, PeriodName = "Current Week" });
            periodes.Add(new PeriodViewModel { PeriodId = 8, PeriodName = "Custome Date Range" });
           // periodes.Add(new PeriodViewModel { PeriodId = 9, PeriodName = "Allocation Round" });
           // periodes.Add(new PeriodViewModel { PeriodId = 10, PeriodName = "Allocation Year + Month" });
            return periodes;
        }
    }
}
