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
    public interface ICommodityTypeRepository : IGenericRepository<CommodityType>,IRepository<CommodityType>
    {
        /// <summary>
        /// Gets the commodity object by Name.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        CommodityType GetCommodityByName(string p);
        /// <summary>
        /// Return all commodity types in CommodityTypeViewModel format 
        /// </summary>
        /// <returns></returns>
        List<ViewModels.Common.CommodityTypeViewModel> GetAllCommodityTypeForReprot();
    }
}
