using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.Web.Reports;

namespace DRMFSS.Web.Controllers.Reports
{
     [Authorize]
    public partial class StockStatusController : BaseController
    {

        public ActionResult Index()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
           // ViewBag.Stock = db.GetStockStatusReport(user.DefaultHub.HubID,).ToList();

            ViewBag.Commodity = repository.Commodity.GetAllParents();
            ViewBag.Stock = repository.Hub.GetStockStatusReport(user.DefaultHub.HubID, 1).ToList();
            ViewBag.CommodityID = 1;
            return View();
        }

        public ActionResult Commodity(int? id)
        {
            if (id != null)
            {
                BLL.UserProfile user =repository.UserProfile.GetUser(User.Identity.Name);
                ViewBag.Stock = repository.Hub.GetStockStatusReport(user.DefaultHub.HubID,id.Value).ToList();
                
                ViewBag.Commodity = repository.Commodity.GetAllParents();
                ViewBag.CommodityID = id;
                return View("Index");
            }
            return Redirect("Index");
        }

         public ActionResult FreeStock()
         {
             BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
             ViewBag.Stock = repository.Hub.GetStatusReportBySI(user.DefaultHub.HubID).ToList();
             return View();
         }

         public ActionResult Receipts()
         {
             BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
             ViewBag.Stock = repository.Hub.GetStatusReportBySI(user.DefaultHub.HubID).ToList();
             return View();
         }

         public ActionResult Dispatch()
         {
             BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
             ViewBag.Stock = repository.Hub.GetDispatchFulfillmentStatus(user.DefaultHub.HubID).ToList();
             var report = new DispatchReport();
             ViewBag.Report = report;
             report.DataSource = ViewBag.Stock;
             report.HubName.Text = UserProfile.DefaultHub.HubNameWithOwner;
             report.ReportDate.Text = string.Format("Generated On: {0}", DateTime.Now.ToString("dd-MMM-yyyy"));
             return View();
         }

         public ActionResult DispatchPartial()
         {
             BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
             ViewBag.Stock = repository.Hub.GetDispatchFulfillmentStatus(user.DefaultHub.HubID).ToList();
             var report = new DispatchReport();
             ViewBag.Report = report;
             report.DataSource = ViewBag.Stock;
             report.HubName.Text = UserProfile.DefaultHub.HubNameWithOwner;
             report.ReportDate.Text = string.Format( "Generated On: {0}", DateTime.Now.ToString("dd-MMM-yyyy"));
             return PartialView();
         }

         public ActionResult ReportViewerExportTo()
         {
             BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
             ViewBag.Stock = repository.Hub.GetDispatchFulfillmentStatus(user.DefaultHub.HubID).ToList();
             var report = new DispatchReport();
             ViewBag.Report = report;
             report.DataSource = ViewBag.Stock;
             report.HubName.Text = UserProfile.DefaultHub.HubNameWithOwner;
             report.ReportDate.Text = string.Format("Generated On: {0}", DateTime.Now.ToString("dd-MMM-yyyy"));
             //TODO: Deal with this.
             return null;
         }
    }
}
