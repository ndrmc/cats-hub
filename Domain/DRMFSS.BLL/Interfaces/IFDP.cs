using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IFDPRepository : IRepository<FDP>
    {
        /// <summary>
        /// Gets the FDPs by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByRegion(int regionId);
        /// <summary>
        /// Gets the FDPs by woreda.
        /// </summary>
        /// <param name="woredaId">The woreda id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByWoreda(int woredaId);
        /// <summary>
        /// Gets the FDPs by zone.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByZone(int zoneId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllFDPForReport();
    }
}
