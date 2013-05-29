using System.Linq;
using System.Web.Mvc;
using System;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Report.Data;
using System.Collections.Generic;
using DRMFSS.Web.Reports;
using DevExpress.XtraReports.UI;

namespace DRMFSS.Web.Controllers.Reports
{
     [Authorize]
    public partial class ReportsController : BaseController
    {
        //
        // GET: /Reports/

        public virtual ActionResult Index()
        {
            return View();
        }

        DRMFSS.BLL.CTSContext db = new BLL.CTSContext();
        public virtual ActionResult SIReport(string siNumber)
        {
            if (!string.IsNullOrEmpty(siNumber))
            {
                // TODO: redo this report
                var dispatches = from dis in db.Dispatches
                                 where dis.DispatchDetails.FirstOrDefault().ToString() == siNumber.Trim()
                                 select dis;

                // TODO: redo this report
                var recieves = from res in db.Receives
                               where res.ReceiveDetails.FirstOrDefault().TransactionGroup.Transactions.FirstOrDefault().ShippingInstruction.Value == siNumber.Trim()
                               select res;

                Models.SIReportModel model = new Models.SIReportModel();
                foreach(BLL.Dispatch p in dispatches)
                {
                    foreach(BLL.DispatchDetail com in p.DispatchDetails)
                    {
                        Models.TransactedStock dis = new Models.TransactedStock();
                        dis.Warehouse = p.Hub.Name;
                        //dis.Store = p.Store.Name;
                        dis.GIN = p.GIN;
                        dis.Commodity = com.Commodity.Name;
                        dis.Date = p.DispatchDate;
                        dis.FDP = p.FDP.Name;
                        //dis.Quantity = com.DispatchedQuantityInMT;
                        dis.Region = p.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                        dis.Woreda = p.FDP.AdminUnit.Name;
                        dis.Zone = p.FDP.AdminUnit.AdminUnit2.Name;
                        model.Dispatched.Add(dis);
                    }
                }
                foreach (BLL.Receive p in recieves)
                {
                    foreach(BLL.ReceiveDetail com in p.ReceiveDetails)
                    {
                        Models.TransactedStock dis = new Models.TransactedStock();
                        dis.Warehouse = p.Hub.Name;
                        //dis.Store = p.Store.Name;
                        dis.GRN = p.GRN;
                        dis.Commodity = com.Commodity.Name;
                        dis.Date = p.ReceiptDate;
                        //dis.Quantity = com.ReceivedQuantityInMT;
                        model.Recieved.Add(dis);
                    }
                }
                return View(model);

            }
            return View(new Models.SIReportModel());
        }

        public static Web.Reports.MasterReportBound GetContainerReport(DRMFSS.BLL.ViewModels.Report.Data.BaseReport baseReportType)
        {
            Web.Reports.MasterReportBound report = new Web.Reports.MasterReportBound();
            report.DataSource = baseReportType;
            return report;
        }

        public static Web.Reports.MasterReportBound GetFreeStockReport(DRMFSS.BLL.ViewModels.Report.Data.FreeStockReport freestockreport)
        {
            Web.Reports.FreeStockReport rpt = new Web.Reports.FreeStockReport();
            rpt.DataSource = freestockreport.Programs;
            Web.Reports.MasterReportBound report = new Web.Reports.MasterReportBound();
            //report.DataSource = freestockreport.Programs ;
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public static Web.Reports.MasterReportBound GetOffloadingReport(DRMFSS.BLL.ViewModels.Report.Data.OffloadingReport offloadingreport)
        {
            Web.Reports.OffLoadingReport rpt = new Web.Reports.OffLoadingReport();
            rpt.DataSource = offloadingreport;
            Web.Reports.MasterReportBound report = new Web.Reports.MasterReportBound();
            report.DataSource = offloadingreport;
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult FreeStock()
        {
            MasterReportBound report= GetFreeStock(new FreeStockFilterViewModel());
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            FreeStockFilterViewModel ViewModel = new FreeStockFilterViewModel(repository, user);
            ViewBag.Filters = ViewModel;
            return View(report);
        }

        public ActionResult FreeStockPartial(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            MasterReportBound report = GetFreeStock(freeStockFilterViewModel);
            ViewBag.ProgramID = freeStockFilterViewModel.ProgramId;
            return PartialView("FreeStockPartial",report);
        }

        public MasterReportBound GetFreeStock(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.FreeStockReport> reports = new List<BLL.ViewModels.Report.Data.FreeStockReport>();
            DRMFSS.BLL.ViewModels.Report.Data.FreeStockReport freestockreport = new BLL.ViewModels.Report.Data.FreeStockReport();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            freestockreport.Programs = repository.Hub.GetFreeStockGroupedByProgram(user.DefaultHub.HubID, freeStockFilterViewModel);
            freestockreport.PreparedBy = user.GetFullName();
            freestockreport.HubName = user.DefaultHub.HubNameWithOwner;
            freestockreport.ReportDate = System.DateTime.Now;
            freestockreport.ReportName = "FreeStockStatusReport";
            freestockreport.ReportTitle = "Free Stock Status";
            reports.Add(freestockreport);

            DRMFSS.Web.Reports.FreeStockReport rpt = new Web.Reports.FreeStockReport() { DataSource = freestockreport.Programs };
            // XtraReport1 rpt = new XtraReport1() { DataSource = freestockreport.Programs[2].Details };
            MasterReportBound report = new MasterReportBound() { DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

         public MasterReportBound GetOffloading(DispatchesViewModel dispatchesViewModel)
           {
               BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
               OffloadingReportMain main = new OffloadingReportMain();
               List<OffloadingReport> reports = repository.Transaction.GetOffloadingReport(user.DefaultHub.HubID, dispatchesViewModel);
               main.reports = reports;
               main.PreparedBy = user.GetFullName();
               main.HubName = user.DefaultHub.HubNameWithOwner;
               main.ReportDate = DateTime.Now;
               main.ReportName = "OffloadingReport";
               main.ReportTitle = "Offloading";
               List<OffloadingReportMain> coll = new List<OffloadingReportMain>();
               coll.Add(main);
               OffLoadingReport rpt = new OffLoadingReport() { DataSource = reports };
               // XtraReport1 rpt = new XtraReport1() { DataSource = freestockreport.Programs[2].Details };
               MasterReportBound report = new MasterReportBound() { Name = "Offloading Report " + DateTime.Now.ToShortDateString(), DataSource = coll };
               report.rptSubReport.ReportSource = rpt;
               return report;
           }

         public ActionResult OffloadingReport()
        {
            MasterReportBound report = GetOffloading(new DispatchesViewModel());
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DispatchesViewModel ViewModel = new DispatchesViewModel(repository, user);
            ViewBag.Filters = ViewModel;
            return View(report);
        }

         public ActionResult OffloadingReportPartial(DispatchesViewModel dispatchesViewModel)
        {
           MasterReportBound report = GetOffloading(dispatchesViewModel);
           return PartialView("OffloadingReportPartial", report);
        }

        public ActionResult FreeStockReportViewerExportTo(FreeStockFilterViewModel freeStockFilterViewModel)
        {
            MasterReportBound report = GetFreeStock(freeStockFilterViewModel);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }

        public ActionResult OffloadingReportViewerExportTo(DispatchesViewModel dispatchesViewModel)
        {
            DRMFSS.Web.Reports.MasterReportBound rep = GetOffloading(dispatchesViewModel);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(rep);
        }

        public ActionResult Receive()
        {
            MasterReportBound report = GetReceiveReportByBudgetYear(new ReceiptsViewModel());
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            ReceiptsViewModel ViewModel = new ReceiptsViewModel(repository, user);
            ViewBag.Filters = ViewModel;
            return View(report);
        }

        public ActionResult ReceivePartial(ReceiptsViewModel receiptsViewModel)
        {
            MasterReportBound report = GetReceiveReportByBudgetYear(receiptsViewModel);
            return PartialView("ReceivePartial", report);
        }

        public MasterReportBound GetReceiveReport(ReceiptsViewModel receiptsViewModel)
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.ReceiveReportMain> reports = new List<ReceiveReportMain>();
            DRMFSS.BLL.ViewModels.Report.Data.ReceiveReportMain receivereport = new BLL.ViewModels.Report.Data.ReceiveReportMain();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            receivereport.receiveReports = repository.Transaction.GetReceiveReport(user.DefaultHub.HubID, receiptsViewModel);//new List<BLL.ViewModels.Report.Data.ReceiveReport>();
            receivereport.PreparedBy = user.GetFullName();
            receivereport.HubName = user.DefaultHub.HubNameWithOwner;
            receivereport.ReportDate = System.DateTime.Now;
            receivereport.ReportCode = DateTime.Now.ToString();
            receivereport.ReportName = "ReceiveReport";
            receivereport.ReportTitle = "Receive Report";

            DRMFSS.Web.Reports.ReceiveReportByBudgetYear rpt = new Web.Reports.ReceiveReportByBudgetYear() { DataSource = receivereport.receiveReports };
            MasterReportBound report = new MasterReportBound() { Name = "Receive Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public MasterReportBound GetReceiveReportByBudgetYear(ReceiptsViewModel receiptsViewModel)
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.ReceiveReportMain> reports = new List<ReceiveReportMain>();
            DRMFSS.BLL.ViewModels.Report.Data.ReceiveReportMain receivereport = new BLL.ViewModels.Report.Data.ReceiveReportMain();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            receivereport.receiveReports = repository.Transaction.GetReceiveReport(user.DefaultHub.HubID, receiptsViewModel);//new List<BLL.ViewModels.Report.Data.ReceiveReport>();
            receivereport.PreparedBy = user.GetFullName();
            receivereport.HubName = user.DefaultHub.HubNameWithOwner;
            receivereport.ReportDate = System.DateTime.Now;
            receivereport.ReportCode = DateTime.Now.ToString();
            receivereport.ReportName = "ReceiveReport";
            receivereport.ReportTitle = "Receive Report";
            reports.Add(receivereport);

           
            DRMFSS.Web.Reports.ReceiveReportByBudgetYear rpt = new Web.Reports.ReceiveReportByBudgetYear() { DataSource = receivereport.receiveReports };
            MasterReportBound report = new MasterReportBound() { Name = "Receive Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult ReceiveReportViewerExportTo(ReceiptsViewModel receiptsViewModel)
        {
            //DRMFSS.Web.Reports.MasterReportBound rep = GetReceiveReport(receiptsViewModel);
            DRMFSS.Web.Reports.MasterReportBound rep = GetReceiveReportByBudgetYear(receiptsViewModel);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(rep);
        }

        #region Distribution

        public ActionResult DistributionReport()
        {
            DistributionViewModel newDistributionViewModel = new DistributionViewModel();
            newDistributionViewModel.PeriodId = (DateTime.Now.Month - 1/3) + 1;// current quarter by default 
            MasterReportBound report = GetDistributionReportPivot(newDistributionViewModel);
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DistributionViewModel ViewModel = new DistributionViewModel(repository, user);
            ViewBag.Filters = ViewModel;
            
            return View(report);
        }

        public ActionResult DistributionReportPartial(DistributionViewModel distributionViewModel)
        {
            MasterReportBound report = GetDistributionReportPivot(distributionViewModel);
            return PartialView("DistributionReportPartial", report);
        }

        public MasterReportBound GetDistributionReport()
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.DistributionReport> reports = new List<BLL.ViewModels.Report.Data.DistributionReport>();
            DRMFSS.BLL.ViewModels.Report.Data.DistributionReport distribution = new BLL.ViewModels.Report.Data.DistributionReport();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            distribution.PreparedBy = user.GetFullName();
            distribution.ReportCode = DateTime.Now.ToString();
            distribution.ReportDate = DateTime.Now;
            distribution.ReportName = "DistributionReport";
            distribution.ReportTitle = "Distribution Report";
            Random ran = new Random(1);
            distribution.Rows = new List<DistributionRows>();
            for (int i = 1; i < 2; i++)
            {
                DistributionRows r = new DistributionRows();
                r.BudgetYear = DateTime.Now.Year;
                r.Region = (i % 2 == 0) ? "Amhara" : "Benshangul";
                r.Program = "Program " + i.ToString();
                r.DistributedAmount = i * decimal.Parse("2340.43674") * 45;
                int month = ran.Next(4);
                r.Quarter = 1;
                distribution.Rows.Add(r);
            }

            reports.Add(distribution);

            DRMFSS.Web.Reports.DistributionReport rpt = new Web.Reports.DistributionReport() { DataSource = reports[0].Rows };
            MasterReportBound report = new MasterReportBound() { Name = "Distribution Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public MasterReportBound GetDistributionReportPivot(DistributionViewModel distributionViewModel)
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.DistributionReport> reports = new List<BLL.ViewModels.Report.Data.DistributionReport>();
            DRMFSS.BLL.ViewModels.Report.Data.DistributionReport distribution = new BLL.ViewModels.Report.Data.DistributionReport();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            distribution.PreparedBy = user.GetFullName();
            distribution.HubName = user.DefaultHub.HubNameWithOwner;
            distribution.ReportCode = DateTime.Now.ToString();
            distribution.ReportDate = DateTime.Now;
            distribution.ReportName = "DistributionReport";
            distribution.ReportTitle = "Distribution Report";
            distribution.Rows = new List<DistributionRows>();
            
            distribution.Rows = repository.Transaction.GetDistributionReport(user.DefaultHub.HubID, distributionViewModel);
             //   new List<DistributionRows>();
            //for (int i = 1; i < 5; i++)
            //{
            //    DistributionRows r = new DistributionRows();
            //    r.BudgetYear = DateTime.Now.Year;
            //    r.Region = (i % 2 == 0) ? "Amhara" : "Benshangul";
            //    r.Program = "Program " + i.ToString();
            //    r.DistributedAmount = i * decimal.Parse("2340.43674") * 45;
            //    int month = i;
            //    if (month == 0)
            //        month++;
            //    r.Month = month.ToString();
            //    r.Quarter = (i % 3 > 0) ? (i / 3) + 1 : i / 3;
            //    distribution.Rows.Add(r);
            //}

            reports.Add(distribution);

            DRMFSS.Web.Reports.DistributionReportPivot rpt = new Web.Reports.DistributionReportPivot();
            rpt.xrPivotGrid1.DataSource = reports[0].Rows;
            MasterReportBound report = new MasterReportBound() { Name = "Distribution Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult DistributionReportViewerExportTo(DistributionViewModel distributionViewModel)
        {
            //DRMFSS.Web.Reports.MasterReportBound rep = GetDistributionReport();
            MasterReportBound report = GetDistributionReportPivot(distributionViewModel);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }

        #endregion

        #region Delivery

        public ActionResult DonationReport()
        {
            MasterReportBound report = GetDonationReport();

            return View(report);
        }

        public ActionResult DonationReportPartial()
        {
            MasterReportBound report = GetDonationReport();
            return PartialView("DistributionReportPartial", report);
        }

        public MasterReportBound GetDonationReport()
        {
            List<DRMFSS.BLL.ViewModels.Report.Data.DeliveryReport> reports = new List<BLL.ViewModels.Report.Data.DeliveryReport>();
            DRMFSS.BLL.ViewModels.Report.Data.DeliveryReport donation = new BLL.ViewModels.Report.Data.DeliveryReport();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            donation.PreparedBy = user.GetFullName();
            donation.ReportCode = DateTime.Now.ToString();
            donation.ReportDate = DateTime.Now;
            donation.ReportName = "DistributionReport";
            donation.ReportTitle = "Distribution Report";
            Random ran = new Random(1);
            donation.Rows = new List<DeliveryRows>();
            for (int i = 1; i < 200; i++)
            {
                DeliveryRows r = new DeliveryRows();
                r.SINumber = "00001283";
                r.Hub = donation.HubName;
                r.DeliveryOrderNumber = i.ToString().PadLeft(8, '0');
                r.HubOwner = "DRMFSS";
                r.PortName = "Djibuti";
                r.ShippedBy = "WFP";
                r.Vessel = "Liberty Sun";
                r.Project = "DRMFSS 4765";
                r.Commodity = "Cereal";
                r.SubCommodity = "Wheat";
                r.WareHouseNumber = i / 50 + 1;
                r.Unit = "mt";
                r.DeliveryBag = 99 * i * decimal.Parse("12");
                r.DeliveryQuantity = 67 * i * decimal.Parse("34.89");
                r.DeliveryNet = 23 * i * decimal.Parse("81");
                r.Donor = "US Aid";
                r.DeliveryType = "Donation";
                r.DeliveryReferenceNumber = i.ToString().PadLeft(8, '0');
                r.Date = DateTime.Now.ToShortDateString();
                r.TransportedBy = ((i % 3 == 0) ? " DRMFSS " : "Another Trasporter");
                r.VehiclePlateNumber = "03-A0012" + (i / 24).ToString();
                donation.Rows.Add(r);
            }

            reports.Add(donation);

            DRMFSS.Web.Reports.DonationReportByProgram rpt = new Web.Reports.DonationReportByProgram() { DataSource = reports[0].Rows };
            MasterReportBound report = new MasterReportBound() { Name = "Donation Report - " + DateTime.Now.ToShortDateString(), DataSource = reports };
            report.rptSubReport.ReportSource = rpt;
            return report;
        }

        public ActionResult DonationReportViewerExportTo()
        {
            MasterReportBound report = GetDonationReport();
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }

        #endregion
    }
}
