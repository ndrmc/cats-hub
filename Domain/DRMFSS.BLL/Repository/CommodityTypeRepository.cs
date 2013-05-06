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
    public partial class CommodityTypeRepository : ICommodityTypeRepository
    {

        /// <summary>
        /// Gets the name of the commodity by.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public CommodityType GetCommodityByName(string p)
        {
            return db.CommodityTypes.FirstOrDefault(n => n.Name == p);
        }


        public List<ViewModels.Common.CommodityTypeViewModel> GetAllCommodityTypeForReprot()
        {
            var commodityTypes = (from c in db.CommodityTypes select new ViewModels.Common.CommodityTypeViewModel() { CommodityTypeId = c.CommodityTypeID, CommodityTypeName = c.Name }).ToList();
            commodityTypes.Insert(0, new ViewModels.Common.CommodityTypeViewModel { CommodityTypeName = "All Commodities" });
            return commodityTypes;
        }
    }
}
