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
    public partial class AuditRepository : IAuditRepository
    {

        public List<FieldChange> GetChanges(string table, string property, string foreignTable, string foreignFeildName, string foreignFeildKey, string key)
        {
            var changes = (from audit in db.Audits
                           where audit.TableName == table && audit.PrimaryKey == key && audit.NewValue.Contains(property)
                           orderby audit.DateTime descending
                           select audit);

            List<FieldChange> filedsList = new List<FieldChange>();
            foreach (Audit a in changes)
            {
                if (foreignTable != null && foreignFeildName != null)
                {
                    filedsList.Add(new FieldChange(a, property, foreignTable, foreignFeildName, foreignFeildKey));
                }
                else
                {
                    filedsList.Add(new FieldChange(a, property));
                }
            }
            return filedsList;
        }
            
    }
}
