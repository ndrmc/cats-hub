using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.ViewModels.Report.Data
{
    public class BaseReport
    {
        public string ReportName { get; set; }
        public string HubName { get; set; }
        public string PreparedBy { get; set; }
        public string ReportTitle { get; set; }
        public string ReportCode { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
