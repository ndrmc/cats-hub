using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DRMFSS.Web.Controllers.Allocations
{
    public class DispatchPlanController : BaseController
    {
        //
        // GET: /DispatchPlan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListRequisitions()
        {
            return PartialView(repository.DispatchAllocation.GetSummaryForUncommitedAllocations(GetCurrentUserProfile().DefaultHub.HubID));
        }

        public ActionResult RequistionDetails(int req)
        {
            return PartialView();
        }

    }
}
