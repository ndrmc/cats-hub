using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class AdjustmentReason
    {
        public AdjustmentReason()
        {
            this.Adjustments = new List<Adjustment>();
        }

        public int AdjustmentReasonID { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public virtual ICollection<Adjustment> Adjustments { get; set; }
    }
}
