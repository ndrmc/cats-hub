using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IConstantsService
    {
        List<ViewModels.Report.CodesViewModel> GetAllCodes();
        List<ViewModels.Report.TypeViewModel> GetAllTypes();
    }
}
