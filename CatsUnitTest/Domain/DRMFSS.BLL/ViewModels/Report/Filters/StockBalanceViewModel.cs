using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Repository;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.BLL.ViewModels.Report
{
    /// <summary>
    /// view for StockBalance view and Wrapping  filtering criteria objects
    /// </summary>
    public class StockBalanceViewModel
    {
        public List<ProgramViewModel> Programs { get; set; }
        public List<CodesViewModel> Cods { get; set; }
        public List<CommodityTypeViewModel> CommodityTypes { get; set; }
        public PeriodViewModel Period { get; set; }
        public List<StoreViewModel> Stores { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime SelectedDate { get; set; }



        public int? ProgramId { get; set; }
        public int? CodesId { get; set; }
        public int? CommodityTypeId { get; set; }
        public int? PeriodId { get; set; }
        public int? StoreId { get; set; }
        public int? ProjectCodeId { get; set; }
        public int? ShippingInstructionId { get; set; }


        public StockBalanceViewModel()
        {
        }
        public StockBalanceViewModel(IUnitOfWork Repository, UserProfile user)
        {
            this.Cods = ConstantsRepository.GetAllCodes();
            this.CommodityTypes = Repository.CommodityType.GetAllCommodityTypeForReprot();
            this.Programs = Repository.Program.GetAllProgramsForReport();
            this.Stores = Repository.Hub.GetAllStoreByUser(user);
        }
    }
}
