using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Period
    {
        public int PeriodID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
    }
}
