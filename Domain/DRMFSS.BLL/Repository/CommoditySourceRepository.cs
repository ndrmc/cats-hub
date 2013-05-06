using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommoditySourceRepository : ICommoditySourceRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.CommoditySourceViewModel> GetAllCommoditySourceForReport()
        {
            var commoditySources = (from c in db.CommoditySources select new ViewModels.Report.CommoditySourceViewModel() { CommoditySourceId = c.CommoditySourceID, CommoditySourceName = c.Name }).ToList();
            commoditySources.Insert(0, new ViewModels.Report.CommoditySourceViewModel { CommoditySourceName = "All Sources" });
            return commoditySources;
        }
    }
}
