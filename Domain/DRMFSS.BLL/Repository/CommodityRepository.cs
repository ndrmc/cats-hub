using System;
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
    public partial class CommodityRepository : ICommodityRepository
    {

        /// <summary>
        /// Gets the name of the commodity by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public BLL.Commodity GetCommodityByName(string name)
        {
            return db.Commodities.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Gets all parents.
        /// </summary>
        /// <returns></returns>
        public List<Commodity> GetAllParents()
        {
            return db.Commodities.Where(c => c.ParentID == null).ToList();
        }

        /// <summary>
        /// Gets all sub commodities.
        /// </summary>
        /// <returns></returns>
        public List<Commodity> GetAllSubCommodities()
        {
            return db.Commodities.Where(c => c.ParentID != null).ToList();
        }

        /// <summary>
        /// Gets all sub commodities by parant id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public List<Commodity> GetAllSubCommoditiesByParantId(int Id)
        {
            return db.Commodities.Where(c => c.ParentID == Id).ToList();
        }



        /// <summary>
        /// Determines whether name is valid for the specified commodity ID.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="Name">The name.</param>
        /// <returns>
        ///   <c>true</c> if  [name is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNameValid(int? CommodityID, string Name)
        {
           return  !(from v in db.Commodities
                           where v.Name == Name && CommodityID != v.CommodityID
                           select v).Any();
        }

        /// <summary>
        /// Determines whether [commodity code is valid] for [the specified commodity ID].
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="CommodityCode">The commodity code.</param>
        /// <returns>
        ///   <c>true</c> if [commodity code is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCodeValid(int? CommodityID, string CommodityCode)
        {
            return !(from v in db.Commodities
                     where v.CommodityCode == CommodityCode && CommodityID != v.CommodityID
                     select v).Any();
        }

        public List<ViewModels.Common.CommodityViewModel> GetAllCommodityForReprot()
        {
            var commodities = (from c in db.Commodities where c.ParentID == null select new ViewModels.Common.CommodityViewModel() { CommodityId = c.CommodityID, CommodityName = c.Name }).ToList();
            commodities.Insert(0, new ViewModels.Common.CommodityViewModel { CommodityName = "All Commodities" });
            return commodities;
        }
    }
}
