using System.Collections.Generic;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Common;
namespace DRMFSS.BLL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>d
    public interface IProjectCodeRepository : IGenericRepository<ProjectCode>,IRepository<ProjectCode>
    {
        /// <summary>
        /// Gets the project code id.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns></returns>
        int GetProjectCodeId(string projectCode);

        /// <summary>
        /// Gets the project code id W ith create.
        /// </summary>
        /// <param name="ProjectNumber">The project number.</param>
        /// <returns></returns>
        ProjectCode GetProjectCodeIdWIthCreate(string ProjectNumber);

        /// <summary>
        /// Gets all project code for report.
        /// </summary>
        /// <returns></returns>
        List<ProjectCodeViewModel> GetAllProjectCodeForReport();

        /// <summary>
        /// Gets the project codes for commodity.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <returns></returns>
        List<ProjectCodeViewModel> GetProjectCodesForCommodity(int hubID, int parentCommodityId);


    }
}
