using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class ReleaseNote
    {
        public int ReleaseNoteID { get; set; }
        public string ReleaseName { get; set; }
        public Nullable<int> BuildNumber { get; set; }
        public System.DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Details { get; set; }
        public int DBuildQuality { get; set; }
        public string Comments { get; set; }
    }
}
