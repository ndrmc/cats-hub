using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.ViewModels.Report
{
    public class AreaPartialViewModel
    {
        public List<AreaViewModel> Region { get; set; }
        public List<AreaViewModel> Zone { get; set; }
        public List<AreaViewModel> Woreda { get; set; }
        public List<AreaViewModel> FDP { get; set; }


        public AreaPartialViewModel()
        {
        }

        public AreaPartialViewModel(IUnitOfWork Repository, UserProfile user)
        {
            this.Region = Repository.AdminUnit.GetAllAreasForReport();
        }
    }
}
