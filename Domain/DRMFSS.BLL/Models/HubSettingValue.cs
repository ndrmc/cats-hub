using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class HubSettingValue
    {
        public int HubSettingValueID { get; set; }
        public int HubSettingID { get; set; }
        public int HubID { get; set; }
        public string Value { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual HubSetting HubSetting { get; set; }
    }
}
