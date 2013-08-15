using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface ISettingRepository : IGenericRepository<Setting>,IRepository<Setting>
    {
        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <returns></returns>
        string GetSettingValue(string Key);
            
    }
}
