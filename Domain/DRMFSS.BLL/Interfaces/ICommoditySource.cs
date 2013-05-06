using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface ICommoditySourceRepository : IRepository<CommoditySource>
    {
        /// <summary>
        /// return all commodity types in CommoditySourceViewModel format 
        /// </summary>
        /// <returns></returns>
        List<DRMFSS.BLL.ViewModels.Report.CommoditySourceViewModel> GetAllCommoditySourceForReport();
    }
}
