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
    public interface IAuditRepository : IRepository<Audit>
    {
        List<FieldChange> GetChanges(string table, string property, string foreignTable, string foreignFeildName, string foreignFeildKey, string key);
    }
}
