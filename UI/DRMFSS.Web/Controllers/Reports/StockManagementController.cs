using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels.Report;

namespace DRMFSS.Web.Controllers.Reports
{
    public class StockManagementController : BaseController
    {
        private IUnitOfWork repository;

        //
        // GET: /StockManagement/
        public StockManagementController()
        {
            repository = new UnitOfWork();
        }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show the ETA, MT Expected, MT + % that has arrived, MT + % still to arrive
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrivalsVsReceipts()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            ArrivalsVsReceiptsViewModel ViewModel = new ArrivalsVsReceiptsViewModel(repository, user);
          
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArrivalsVsReceiptsReport(ArrivalsVsReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : repository.CommoditySource.GetAll().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Receipts()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            ReceiptsViewModel ViewModel = new ReceiptsViewModel(repository, user);
            

            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReceiptsReport(ReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : repository.CommoditySource.GetAll().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StockBalance()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            StockBalanceViewModel ViewModel = new StockBalanceViewModel(repository, user);

            
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult StockBalanceReport(StockBalanceViewModel viewModel)
        {

            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView() ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Dispatches()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DispatchesViewModel ViewModel = new DispatchesViewModel(repository,user);
            
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DispatchesReport(DispatchesViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CommittedVsDispatched()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            CommittedVsDispatchedViewModel ViewModel = new CommittedVsDispatchedViewModel(repository,user);
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult CommittedVsDispatchedReport(CommittedVsDispatchedViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult InTransitReprot(InTransitViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult InTransit()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            InTransitViewModel ViewModel = new InTransitViewModel(repository,user);
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatch()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DeliveryAgainstDispatchViewModel ViewModel = new DeliveryAgainstDispatchViewModel(repository, user);
            
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatchReport(DeliveryAgainstDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatch()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DistributionDeliveryDispatchViewModel ViewModel = new DistributionDeliveryDispatchViewModel(repository,user);
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatchReport(DistributionDeliveryDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionByOwner()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            DistributionByOwnerViewModel ViewModel = new DistributionByOwnerViewModel(repository,user);
           
            return View(ViewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionByOwnerReport(DistributionByOwnerViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : repository.Program.GetAll().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : repository.CommodityType.GetAll().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }



        // partial views 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectCode()
        {
            ViewBag.ProjectCode = new SelectList(repository.ProjectCode.GetAllProjectCodeForReport(), "ProjectCodeId", "ProjectName");
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ShippingInstruction()
        {
            ViewBag.ShippingInstruction = new SelectList(repository.ShippingInstruction.GetAllShippingInstructionForReport(), "ShippingInstructionId", "ShippingInstructionName");
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AreaPartial()
        {
            
            return PartialView();
        }

        public ActionResult CustomeDate()
        {
            return PartialView();
        }

        public ActionResult MonthToDate()
        {
            return PartialView();
        }
    }
}
