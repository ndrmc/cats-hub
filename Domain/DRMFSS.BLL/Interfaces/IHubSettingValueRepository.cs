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
    public  interface IHubSettingValueRepository : IGenericRepository<HubSettingValue>,IRepository<HubSettingValue>
    {
            
    }
}
