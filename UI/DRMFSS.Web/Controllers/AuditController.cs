using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
    public class AuditController : BaseController
    {

        IUnitOfWork repository = new UnitOfWork();

        public AuditController()
        {
        }

        public AuditController(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        //
        // GET: /Audit/

        //public ActionResult Audits(int id, string tableName, string fieldName)
        //{
        //    List<BLL.FieldChange> changes = BLL.Audit.GetChanges(id, tableName, fieldName);
        //    return PartialView("FieldAudits", changes);
        //}
        //TODO : Add a role here to the authorization
        [Authorize]
        public ActionResult Audits(string id, string tableName, string fieldName, string foreignTable, string foreignFeildName,string foreignFeildKey)
        {
            List<BLL.FieldChange> changes = repository.Audit.GetChanges(tableName, fieldName, foreignTable, foreignFeildName, foreignFeildKey, id.ToString());
            return PartialView("FieldAudits", changes);
        }
    }
}
