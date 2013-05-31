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
    public partial class CommoditySourceRepository :GenericRepository<CTSContext,CommoditySource>, ICommoditySourceRepository
    {
        public CommoditySourceRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
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

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.CommoditySources.Remove(original);
            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public CommoditySource FindById(int id)
        {
            return db.CommoditySources.SingleOrDefault(t => t.CommoditySourceID == id);
        }

        public CommoditySource FindById(System.Guid id)
        {
            return null;
        }
    }
}
