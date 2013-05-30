using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Setting
    {
        public int SettingID { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Option { get; set; }
        public string Type { get; set; }
    }
}
