using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL.Services;
using DRMFSS.BLL.ViewModels.Report;

namespace DRMFSS.Web.Controllers.Reports
{
    public class StockManagementController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IProgramService _programService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly ICommoditySourceService _commoditySourceService;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;

        //
        // GET: /StockManagement/
        public StockManagementController(IUserProfileService userProfileService, IProgramService programService,
            ICommodityTypeService commodityTypeService, ICommoditySourceService commoditySourceService, 
            IProjectCodeService projectCodeService, IShippingInstructionService shippingInstructionService)
        {
            _userProfileService = userProfileService;
            _programService = programService;
            _commodityTypeService = commodityTypeService;
            _commoditySourceService = commoditySourceService;
            _projectCodeService = projectCodeService;
            _shippingInstructionService = shippingInstructionService;
        }

        /// <summary>
        /// Show the ETA, MT Expected, MT + % that has arrived, MT + % still to arrive
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrivalsVsReceipts()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new ArrivalsVsReceiptsViewModel(repository, user);
          
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArrivalsVsReceiptsReport(ArrivalsVsReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : _commoditySourceService.GetAllCommoditySource().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Receipts()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new ReceiptsViewModel(repository, user);
            

            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReceiptsReport(ReceiptsViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            ViewBag.CommoditySources = viewModel.CommoditySourceId == 0 ? "All" : _commoditySourceService.GetAllCommoditySource().Where(c => c.CommoditySourceID == viewModel.CommoditySourceId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StockBalance()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new StockBalanceViewModel(repository, user);

            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult StockBalanceReport(StockBalanceViewModel viewModel)
        {

            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView() ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Dispatches()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new DispatchesViewModel(repository,user);
            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DispatchesReport(DispatchesViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CommittedVsDispatched()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new CommittedVsDispatchedViewModel(repository,user);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult CommittedVsDispatchedReport(CommittedVsDispatchedViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult InTransitReprot(InTransitViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult InTransit()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new InTransitViewModel(repository,user);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatch()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new DeliveryAgainstDispatchViewModel(repository, user);
            
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DeliveryAgainstDispatchReport(DeliveryAgainstDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == null ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatch()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new DistributionDeliveryDispatchViewModel(repository,user);
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionDeliveryDispatchReport(DistributionDeliveryDispatchViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionByOwner()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var viewModel = new DistributionByOwnerViewModel(repository,user);
           
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult DistributionByOwnerReport(DistributionByOwnerViewModel viewModel)
        {
            ViewBag.Program = viewModel.ProgramId == null ? "All" : _programService.GetAllProgram().Where(c => c.ProgramID == viewModel.ProgramId).Select(c => c.Name).Single();
            ViewBag.CommodityTypes = viewModel.CommodityTypeId == 0 ? "All" : _commodityTypeService.GetAllCommodityType().Where(c => c.CommodityTypeID == viewModel.CommodityTypeId).Select(c => c.Name).Single();
            return PartialView();
        }



        // partial views 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectCode()
        {
            ViewBag.ProjectCode = new SelectList(_projectCodeService.GetAllProjectCodeForReport(), "ProjectCodeId", "ProjectName");
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ShippingInstruction()
        {
            ViewBag.ShippingInstruction = new SelectList(_shippingInstructionService.GetAllShippingInstructionForReport(), "ShippingInstructionId", "ShippingInstructionName");
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
