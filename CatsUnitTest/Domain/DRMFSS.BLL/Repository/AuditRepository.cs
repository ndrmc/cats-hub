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
    public partial class AuditRepository : GenericRepository<CTSContext,Audit>,IAuditRepository
    {
        public AuditRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
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


        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Audits.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Audit FindById(int id)
        {
            return null;
        }

        public Audit  FindById(System.Guid id)
        { return db.Audits.FirstOrDefault(t => t.AuditID == id);
           
        }
    }
}
