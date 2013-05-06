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
    public interface IDonorRepository : IRepository<Donor>
    {
        /// <summary>
        /// Gets all source donor for report.
        /// </summary>
        /// <returns></returns>
        List<DonorViewModel> GetAllSourceDonorForReport();

        /// <summary>
        /// Gets all responsible donor for report.
        /// </summary>
        /// <returns></returns>
        List<DonorViewModel> GetAllResponsibleDonorForReport();

        /// <summary>
        /// Determines whether [is code valid] [the specified donor code].
        /// </summary>
        /// <param name="DonorCode">The donor code.</param>
        /// <param name="DonorID">The donor ID.</param>
        /// <returns></returns>
        bool IsCodeValid(string DonorCode, int? DonorID);
    }
}
