using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Partition
    {
        public int PartitionID { get; set; }
        public int HubID { get; set; }
        public string ServerUserName { get; set; }
        public System.DateTime PartitionCreatedDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<System.DateTime> LastSyncTime { get; set; }
        public bool HasConflict { get; set; }
        public bool IsActive { get; set; }
    }
}
