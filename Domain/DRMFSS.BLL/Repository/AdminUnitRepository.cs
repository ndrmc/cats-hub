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
    public partial class AdminUnitRepository : GenericRepository<CTSContext,AdminUnit>,IAdminUnitRepository
    {
        public AdminUnitRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public const int  WOREDATYPE = 4;
        public const int REGIONTYPE = 2;
        public const int ZONETYPE = 3;

        /// <summary>
        /// Gets the type of the by unit.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns> List<AdminUnit>returns>
        public List<AdminUnit> GetByUnitType(int typeId)
        {
            return (db.AdminUnits.Where(p => p.AdminUnitTypeID == typeId)).ToList();
        }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns>List<AdminUnit></returns>
        public List<AdminUnit> GetRegions()
        {

            return new CTSContext().AdminUnits
                .Where(u => u.AdminUnitTypeID == 2).ToList();

        }

        /// <summary>
        /// Gets the region by zone id.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        public int GetRegionByZoneId(int zoneId)
        {
            return (from u in new CTSContext().AdminUnits
                    where u.AdminUnitID == zoneId
                    select u.ParentID.Value).Single();
        }

        /// <summary>
        /// Gets the admin unit types.
        /// </summary>
        /// <returns></returns>
        public List<AdminUnitType> GetAdminUnitTypes()
        {
            return db.AdminUnitTypes.Where(a => a.AdminUnitTypeID > 1).ToList();
        }

        /// <summary>
        /// Gets the type of the admin unit.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public AdminUnitType GetAdminUnitType(int id)
        {
            return db.AdminUnitTypes.Include("AdminUnits").SingleOrDefault(a => a.AdminUnitTypeID == id);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="unitId">The unit id.</param>
        /// <returns></returns>
        public List<AdminUnit> GetChildren(int unitId)
        {
            return db.AdminUnits.Include("AdminUnit2").Where(item => item.ParentID == unitId).ToList();
        }

        /// <summary>
        /// Gets all woredas.
        /// </summary>
        /// <returns></returns>
        public List<AdminUnit> GetAllWoredas()
        {
            return new CTSContext().AdminUnits.Include("AdminUnit2").
                Where(p => p.AdminUnitTypeID == WOREDATYPE).OrderBy(q => q.Name).ToList();
        }

        /// <summary>
        /// Gets the woredas by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public List<AdminUnit> GetWoredasByRegion(int regionId)
        {
            return new CTSContext().AdminUnits.Include("AdminUnit2").
                Where(p => p.AdminUnitTypeID == WOREDATYPE && p.AdminUnit2.ParentID == regionId).OrderBy(q => q.Name).ToList();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.AreaViewModel> GetAllAreasForReport()
        {
            var Areas = (from c in db.AdminUnits where c.AdminUnitTypeID.Value == 2 select new ViewModels.Report.AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return Areas;
        }

        public List<ViewModels.Report.AreaViewModel> GetZonesForReport(int AreaId)
        {
            var Areas = (from c in db.AdminUnits where c.AdminUnitTypeID == 3 && c.ParentID == AreaId  select new ViewModels.Report.AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return Areas;
        }

        public List<ViewModels.Report.AreaViewModel> GetWoredasForReport(int AreaId)
        {
            var Areas = (from c in db.AdminUnits where c.AdminUnitTypeID == 4 && c.ParentID == AreaId select new ViewModels.Report.AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return Areas;
        }

        public IEnumerable<TreeViewModel> GetTreeElts(int adminunitParentId, int hubId)
        {
   
            var UnclosedDispatchAllocations = (from dAll in db.DispatchAllocations
                                                where dAll.ShippingInstructionID.HasValue && dAll.ProjectCodeID.HasValue
                                                && hubId == dAll.HubID && dAll.IsClosed == false
                                                select dAll);

            BLL.AdminUnit adminUnit = repository.AdminUnit.FindById(adminunitParentId);

            if (adminUnit.AdminUnitType.AdminUnitTypeID == 1)//by region
                return (from Unc in UnclosedDispatchAllocations
                                               where Unc.FDP.AdminUnit.AdminUnit2.AdminUnit2.ParentID == adminunitParentId
                                               group Unc by new {Unc.FDP.AdminUnit.AdminUnit2.AdminUnit2} into b
                                               select new TreeViewModel
                                                          {
                                                                Name = b.Key.AdminUnit2.Name,
                                                                Value = b.Key.AdminUnit2.AdminUnitID,
                                                                Enabled = true,
                                                                Count = b.Count()
                                                          }).Union(from ad in db.AdminUnits
                                                                       where ad.ParentID == adminunitParentId
                                                                       select new TreeViewModel
                                                                            {
                                                                                Name = ad.Name,
                                                                                Value = ad.AdminUnitID,
                                                                                Enabled = true,
                                                                                Count = 0
                                                                            }
                                                                        );
            else if (adminUnit.AdminUnitType.AdminUnitTypeID == 2)//by zone
                return 
                                              (from Unc in UnclosedDispatchAllocations
                                               where Unc.FDP.AdminUnit.AdminUnit2.ParentID == adminunitParentId
                                               group Unc by new {Unc.FDP.AdminUnit.AdminUnit2} into b
                                               select new TreeViewModel
                                                          {
                                                                Name = b.Key.AdminUnit2.Name,
                                                                Value = b.Key.AdminUnit2.AdminUnitID,
                                                                Enabled = true,
                                                                Count = b.Count()
                                                          }).Union(from ad in db.AdminUnits
                                                                       where ad.ParentID == adminunitParentId
                                                                       select new TreeViewModel
                                                                            {
                                                                                Name = ad.Name,
                                                                                Value = ad.AdminUnitID,
                                                                                Enabled = true,
                                                                                Count = 0
                                                                            }
                                                                        );
            else //if (adminUnit.AdminUnitType.AdminUnitTypeID == 4)//by woreda
                return
                              (from Unc in UnclosedDispatchAllocations
                               where Unc.FDP.AdminUnit.ParentID == adminunitParentId
                               group Unc by new { Unc.FDP.AdminUnit } into b
                               select new TreeViewModel
                               {
                                   Name = b.Key.AdminUnit.Name,
                                   Value = b.Key.AdminUnit.AdminUnitID,
                                   Enabled = true,
                                   Count = b.Count()
                               }).Union(from ad in db.AdminUnits
                                        where ad.ParentID == adminunitParentId
                                        select new TreeViewModel
                                        {
                                            Name = ad.Name,
                                            Value = ad.AdminUnitID,
                                            Enabled = true,
                                            Count = 0
                                        }
         );


        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.AdminUnits.Remove(original);
            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public AdminUnit FindById(int id)
        {
            return db.AdminUnits.FirstOrDefault(t => t.AdminUnitID == id);
        }

        public AdminUnit FindById(System.Guid id)
        {
            return null;
        }
    }
}
