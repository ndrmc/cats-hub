using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Common;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProgramRepository : IProgramRepository
    {

        public List<ProgramViewModel> GetAllProgramsForReport()
        {
            var programs = (from c in db.Programs select new ProgramViewModel() { ProgramId = c.ProgramID, ProgramName = c.Name }).ToList();
            programs.Insert(0 , new ProgramViewModel { ProgramName = "All Programs" });
            return programs;
        }
    }
}
