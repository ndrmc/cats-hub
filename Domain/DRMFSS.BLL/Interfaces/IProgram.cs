using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IProgramRepository : IRepository<Program>
    {
        /// <summary>
        /// return all programs in view model structure 
        /// </summary>
        /// <returns></returns>
        List<DRMFSS.BLL.ViewModels.Common.ProgramViewModel> GetAllProgramsForReport();
            
    }
}
