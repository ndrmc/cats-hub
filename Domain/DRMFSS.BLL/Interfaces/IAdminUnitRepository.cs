using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.Repository;
using DRMFSS.BLL.ViewModels.Report;


namespace DRMFSS.BLL.Interfaces
{

    public interface IAdminUnitRepository : IGenericRepository<AdminUnit>,IRepository<AdminUnit>
    {
        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        List<AdminUnit> GetRegions();
        /// <summary>
        /// Gets the region by zone id.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        int GetRegionByZoneId(int zoneId);
        /// <summary>
        /// Gets the admin unit types.
        /// </summary>
        /// <returns></returns>
        List<AdminUnitType> GetAdminUnitTypes();
        /// <summary>
        /// Gets the type of the admin unit.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        AdminUnitType GetAdminUnitType(int id);
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="unitId">The unit id.</param>
        /// <returns></returns>
        List<AdminUnit> GetChildren(int unitId);
        /// <summary>
        /// Gets all woredas.
        /// </summary>
        /// <returns></returns>
        List<AdminUnit> GetAllWoredas();
        /// <summary>
        /// Gets the woredas by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        List<AdminUnit> GetWoredasByRegion(int regionId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllAreasForReport();

        IEnumerable<TreeViewModel> GetTreeElts(int p, int hubId);

        List<AreaViewModel> GetZonesForReport(int AreaId);

        List<AreaViewModel> GetWoredasForReport(int AreaId);
    }
}
