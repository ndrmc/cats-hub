using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRMFSS.BLL
{
    public partial class Period
    {
        [Key]
        public int PeriodID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
    }
}
