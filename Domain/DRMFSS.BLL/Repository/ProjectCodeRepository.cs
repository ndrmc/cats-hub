using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.BLL.Repository
{


    /// <summary>
    /// 
    /// </summary>
    public partial class ProjectCodeRepository : IProjectCodeRepository
    {
        /// <summary>
        /// Gets the project code id.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns></returns>
        public int GetProjectCodeId(string projectCode)
        {
            ProjectCode project = (from i in db.ProjectCodes
                                   where i.Value.ToUpper() == projectCode.ToUpper()
                                   select i).SingleOrDefault();
            if (project == null)
            {
                return 0;
            }else
            {
                return project.ProjectCodeID;
            }
        }

        /// <summary>
        /// Gets the project code id W ith create.
        /// </summary>
        /// <param name="projectNumber">The project number.</param>
        /// <returns></returns>
        public ProjectCode GetProjectCodeIdWIthCreate(string projectNumber)
        {

            ProjectCode project = (from i in db.ProjectCodes
                                   where i.Value.ToUpper() == projectNumber.ToUpper()
                                   select i).SingleOrDefault();
            if (project != null)
            {
                return project;
            }
            else
            {
                ProjectCode newProjectCode = new ProjectCode()
                {
                    Value = projectNumber.ToUpperInvariant()
                };
                db.ProjectCodes.AddObject(newProjectCode);
                db.SaveChanges();
                return newProjectCode;
            }

        }

        /// <summary>
        /// Gets all the project code in ProejctCodeViewModel 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Common.ProjectCodeViewModel> GetAllProjectCodeForReport()
        {
            var projectCodes = (from c in db.ProjectCodes select new ViewModels.Common.ProjectCodeViewModel() { ProjectCodeId = c.ProjectCodeID, ProjectName = c.Value }).ToList();
            return projectCodes;
        }

        public List<ProjectCodeViewModel> GetProjectCodesForCommodity(int hubID, int parentCommodityId)
        {
            var projectCodes = (from v in db.Transactions
                                where v.ParentCommodityID == parentCommodityId && v.HubID == hubID
                                select
                                    new ProjectCodeViewModel
                                        {ProjectCodeId = v.ProjectCodeID, ProjectName = v.ProjectCode.Value}).Distinct()
                .ToList();
            return projectCodes;
        } 
    }
}
