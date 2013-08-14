
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ICommodityTypeService
    {

        bool AddCommodityType(CommodityType commodityType);
        bool DeleteCommodityType(CommodityType commodityType);
        bool DeleteById(int id);
        bool EditCommodityType(CommodityType commodityType);
        CommodityType FindById(int id);
        List<CommodityType> GetAllCommodityType();
        List<CommodityType> FindBy(Expression<Func<CommodityType, bool>> predicate);

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


