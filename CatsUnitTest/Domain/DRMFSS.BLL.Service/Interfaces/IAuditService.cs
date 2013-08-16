using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IAuditService : IDisposable
    {
        bool AddAudit(Audit audit);
        bool DeleteAudit(Audit audit);
        bool DeleteById(int id);
        bool EditAudit(Audit audit);
        Audit FindById(int id);
        List<Audit> GetAllAudit();
        List<Audit> FindBy(Expression<Func<Audit, bool>> predicate);
    }
}
