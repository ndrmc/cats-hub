using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class HubSetting
    {
        public HubSetting()
        {
            this.HubSettingValues = new List<HubSettingValue>();
        }

        public int HubSettingID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string Options { get; set; }
        public virtual ICollection<HubSettingValue> HubSettingValues { get; set; }
    }
}
