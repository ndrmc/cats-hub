using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SettingRepository : ISettingRepository
    {

        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <returns></returns>
        public string GetSettingValue(string Key)
        {
            string settingValue = (from v in db.Settings
                                   where v.Key == Key
                                   select v.Value).FirstOrDefault();
            return settingValue;
        }
    }
}
