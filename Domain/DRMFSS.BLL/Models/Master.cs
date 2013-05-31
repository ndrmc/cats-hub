using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Master
    {
        public Master()
        {
            this.Details = new List<Detail>();
        }

        public int MasterID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
