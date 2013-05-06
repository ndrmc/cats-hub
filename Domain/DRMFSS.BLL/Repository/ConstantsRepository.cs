using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public static class ConstantsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.Report.CodesViewModel> GetAllCodes()
        {
            List<ViewModels.Report.CodesViewModel> codes = new List<ViewModels.Report.CodesViewModel>();
            codes.Add(new ViewModels.Report.CodesViewModel { CodesId = 0, CodesName = "All Codes"});
            codes.Add(new ViewModels.Report.CodesViewModel { CodesId = 1, CodesName = "Particular SI"});
            codes.Add(new ViewModels.Report.CodesViewModel { CodesId = 2, CodesName = "Particular PC"});
            return codes;
        }

        public static List<ViewModels.Report.TypeViewModel> GetAllTypes()
        {
            List<ViewModels.Report.TypeViewModel> types = new List<ViewModels.Report.TypeViewModel>();
            types.Add(new ViewModels.Report.TypeViewModel { TypeId = 0, TypeName = "All Types" });
            types.Add(new ViewModels.Report.TypeViewModel { TypeId = 1, TypeName = "FDP Dispatch" });
            types.Add(new ViewModels.Report.TypeViewModel { TypeId = 2, TypeName = "Transfer To Other Hub" });
            return types;
        }


    }
}
