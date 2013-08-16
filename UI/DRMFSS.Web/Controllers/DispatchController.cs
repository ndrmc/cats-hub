using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;
using DRMFSS.BLL.ViewModels;
using Newtonsoft.Json;
using Telerik.Web.Mvc;
using DRMFSS.Web.Models;
using System;
using DRMFSS.BLL.ViewModels.Dispatch;

namespace DRMFSS.Web.Controllers
{ 
    [Authorize]
    public class DispatchController : BaseController
    {
        private readonly IDispatchAllocationService _dispatchAllocationService;
        private readonly IDispatchService _dispatchService;
        private readonly IUserProfileService _userProfileService;
        private readonly IOtherDispatchAllocationService _otherDispatchAllocationService;
        private readonly IDispatchDetailService _dispatchDetailService;
        private readonly IUnitService _unitService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IProgramService _programService;
        private readonly ITransporterService _transporterService;
        private readonly IPeriodService _periodService;
        private readonly ICommodityService _commodityService;
        private readonly ITransactionService _transactionService;
        private readonly IStoreService _storeService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IHubService _hubService;
        private readonly IFDPService _fdpService;
        private readonly IProjectCodeService _projectCodeService;
        private readonly IShippingInstructionService _shippingInstructionService;

        public DispatchController(IDispatchAllocationService dispatchAllocationService, IDispatchService dispatchService,
            IUserProfileService userProfileService, IOtherDispatchAllocationService otherDispatchAllocationService,
            IDispatchDetailService dispatchDetailService, IUnitService unitService, ICommodityTypeService commodityTypeService,
            IProgramService programService, ITransporterService transporterService, IPeriodService periodService, 
            ICommodityService commodityService, ITransactionService transactionService, IStoreService storeService,
            IAdminUnitService adminUnitService, IHubService hubService, IFDPService fdpService, 
            IProjectCodeService projectCodeService, IShippingInstructionService shippingInstructionService)
        {
            _dispatchAllocationService = dispatchAllocationService;
            _dispatchService = dispatchService;
            _userProfileService = userProfileService;
            _otherDispatchAllocationService = otherDispatchAllocationService;
            _dispatchDetailService = dispatchDetailService;
            _unitService = unitService;
            _commodityTypeService = commodityTypeService;
            _programService = programService;
            _transporterService = transporterService;
            _periodService = periodService;
            _commodityService = commodityService;
            _transactionService = transactionService;
            _storeService = storeService;
            _adminUnitService = adminUnitService;
            _hubService = hubService;
            _fdpService = fdpService;
            _projectCodeService = projectCodeService;
            _shippingInstructionService = shippingInstructionService;
        }


        public ViewResult Index()
        {
            var model = new DispatchHomeViewModel(repository, _userProfileService.GetUser(User.Identity.Name));
            return View(model);
        }

        [GridAction]
        public ActionResult GetFdpAllocations(bool? closed, int? adminUnitID, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var fdpAllocations = _dispatchAllocationService.GetCommitedAllocationsByHubDetached(user.DefaultHub.HubID, user.PreferedWeightMeasurment, closed, adminUnitID, commodityType);
            return View(new GridModel(fdpAllocations));
        }

        [GridAction]
        public ActionResult GetLoanAllocations(bool? closed, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var loanAllocations = _otherDispatchAllocationService.GetCommitedLoanAllocationsDetached(user, closed, commodityType);
            return View(new GridModel(loanAllocations));
        }

        [GridAction]
        public ActionResult GetTransferAllocations(bool? closed, int? commodityType)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            var transferAllocations = _otherDispatchAllocationService.GetCommitedTransferAllocationsDetached(user, closed, commodityType);
            return View(new GridModel(transferAllocations));
        }

        [GridAction]
        public ActionResult DispatchListGrid(string dispatchAllocationID)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            //TODO cascade using allocation id
            var dispatchs = _dispatchService.ByHubIdAndAllocationIDetached(user.DefaultHub.HubID, Guid.Parse(dispatchAllocationID));
            return View(new GridModel(dispatchs));
        }

        [GridAction]
        public ActionResult DispatchOtherListGrid(string otherDispatchAllocationID)
        {
            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            //TODO cascade using allocation id
            List<DispatchModelModelDto> otherDispatchs = _dispatchService.ByHubIdAndOtherAllocationIDetached(user.DefaultHub.HubID, Guid.Parse(otherDispatchAllocationID));
            return View(new GridModel(otherDispatchs));
        }
        
        [GridAction]
        public ActionResult DispatchListGridListGrid(string dispatchID)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            //(user.DefaultHub.HubID)
            var receiveDetails = _dispatchDetailService.ByDispatchIDetached(Guid.Parse(dispatchID), user.PreferedWeightMeasurment);
            return View(new GridModel(receiveDetails));
        }
        public ViewResult Log()
        {
            var dispatches = _dispatchService.GetAllDispatch();
            var user = _userProfileService.GetUser(User.Identity.Name);
            return View(dispatches.Where(p => p.HubID == user.DefaultHub.HubID).ToList());
        }

        //GIN unique validation

        public virtual ActionResult NotUnique(string gin, string dispatchID)
        {

            var dispatch = _dispatchService.GetDispatchByGIN(gin);
            var user = _userProfileService.GetUser(User.Identity.Name);
            
            Guid guidParse;
            if (Guid.TryParse(dispatchID, out guidParse))
            {
                guidParse = Guid.Parse(dispatchID);
            }

            if (dispatch == null || ((dispatch.DispatchID != Guid.Empty) && (dispatch.DispatchID == guidParse)))// new one or edit no problem 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {

                if (dispatch.HubID == user.DefaultHub.HubID)
                {
                    return Json(string.Format("{0} is invalid, there is an existing record with the same GIN", gin),
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(string.Format("{0} is invalid, there is an existing record with the same GIN at another Warehouse", gin),
                    JsonRequestBehavior.AllowGet);
                }
            }
        }
        //
        // GET: /Dispatch/Create

        public   ActionResult Create(string ginNo, int type)
        {

            ViewBag.Units = _unitService.GetAllUnit();

            BLL.Dispatch dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            if (dispatch != null)
            {
                if (user.DefaultHub != null && user.DefaultHub.HubID == dispatch.HubID)
                {
                    PrepareEdit(dispatch, user, type);
                    Models.DispatchModel dis = Models.DispatchModel.GenerateDispatchModel(dispatch, repository);

                    return View(dis);
                }
                else
                {
 
                    PrepareCreate(type);
                    List<Models.DispatchDetailModel> comms = new List<Models.DispatchDetailModel>();
                    ViewBag.SelectedCommodities = comms;
                    DispatchModel  theViewModel = new Models.DispatchModel() { Type = type };
                    theViewModel.DispatchDetails = comms;
                    ViewBag.Message = "The selected GIN Number doesn't exist on your default warehouse. Try changing your default warehouse.";
                    return View(theViewModel);
                }
            }
            else
            {
                PrepareCreate(type);
                List<Models.DispatchDetailModel> comms = new List<Models.DispatchDetailModel>();
                ViewBag.SelectedCommodities = comms;
                DispatchModel theViewModel = new Models.DispatchModel() { Type = type };
                theViewModel.DispatchDetails = comms;

                if (Request["type"] != null && Request["allocationId"] != null)
                {
                    var allocationId= Guid.Parse(Request["allocationId"]);
                    int allocationTypeId = Convert.ToInt32(Request["type"]);

                    if(allocationTypeId == 1)//to FDP
                    {
                        DispatchAllocation toFDPDispatchAllocation = _dispatchAllocationService.FindById(allocationId);
                        
                        theViewModel.FDPID = toFDPDispatchAllocation.FDPID;
                        PrepareFDPForEdit(toFDPDispatchAllocation.FDPID);
                        
                        theViewModel.RequisitionNo = toFDPDispatchAllocation.RequisitionNo;
                        theViewModel.BidNumber = toFDPDispatchAllocation.BidRefNo;
                        theViewModel.SINumber = toFDPDispatchAllocation.ShippingInstruction.Value;
                        theViewModel.ProjectNumber = toFDPDispatchAllocation.ProjectCode.Value;

                        theViewModel.CommodityTypeID = toFDPDispatchAllocation.Commodity.CommodityTypeID;
                        ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name",toFDPDispatchAllocation.Commodity.CommodityTypeID);
                       
                        if (toFDPDispatchAllocation.ProgramID.HasValue)
                        {
                            theViewModel.ProgramID = toFDPDispatchAllocation.ProgramID.Value;
                            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", theViewModel.ProgramID);
                        }
                        if (toFDPDispatchAllocation.TransporterID.HasValue)
                            theViewModel.TransporterID = toFDPDispatchAllocation.TransporterID.Value;
                            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", theViewModel.TransporterID);
                        if (toFDPDispatchAllocation.Year.HasValue)
                            theViewModel.Year = toFDPDispatchAllocation.Year.Value;
                                    var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
                            ViewBag.Year = new SelectList(years, "Id", "Name"); 
                            ViewBag.Year = new SelectList(years, "Id", "Name",theViewModel.Year);            
                        if (toFDPDispatchAllocation.Month.HasValue)
                            theViewModel.Month = toFDPDispatchAllocation.Month.Value;
                        var months = (from y in _periodService.GetMonths(theViewModel.Year)
                                      select new { Name = y, Id = y }).ToList();
                        ViewBag.Month = new SelectList(months, "Id", "Name", theViewModel.Month);
                        if (toFDPDispatchAllocation.Round.HasValue)
                            theViewModel.Round = toFDPDispatchAllocation.Round.Value;
                        theViewModel.DispatchAllocationID = toFDPDispatchAllocation.DispatchAllocationID;

                    }
                    else //allocationTypeId == 2
                    {
                        var otherDispatchAllocation = _otherDispatchAllocationService.FindById(allocationId);
                        theViewModel.ToHubID = otherDispatchAllocation.ToHubID;
                        theViewModel.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                        theViewModel.ProjectNumber = otherDispatchAllocation.ProjectCode.Value;
                        theViewModel.ProgramID = otherDispatchAllocation.ProgramID;

                        theViewModel.CommodityTypeID = otherDispatchAllocation.Commodity.CommodityTypeID;
                        ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name", otherDispatchAllocation.Commodity.CommodityTypeID);

                        ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", theViewModel.ProgramID);
                        theViewModel.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                    }

                }

                return View(theViewModel);
            }
        } 

        //
        // POST: /Dispatch/Create

        [GridAction]
        public   ActionResult SelectDispatchsCommodities(string dispatchId)
        {
            var commodities = new List<Models.DispatchDetailModel>();
            if (dispatchId != null)
            {
                BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                commodities = DispatchDetailModel.GenerateDispatchDetailModels(_dispatchService.FindById(Guid.Parse(dispatchId)).DispatchDetails);
                //commodities = (from c in repository.DispatchDetail.GetDispatchDetail(Guid.Parse(dispatchId))
                //              select new Models.DispatchDetailModel()
                //              {
                //                  CommodityID = c.CommodityID,
                //                  CommodityName = c.Commodity.Name,
                //                  Description = c.Description,
                //                  Id = c.DispatchDetailID,
                //                  DispatchedQuantityMT = c.DispatchedQuantityInMT,
                //                  RequestedQuantityMT = c.RequestedQuantityInMT,
                //                  RequestedQuantity = c.RequestedQunatityInUnit,
                //                  DispatchedQuantity = c.DispatchedQuantityInUnit,
                //                  Unit = c.UnitID,
                //                 // DispatchID = c.DispatchID
                //              }).ToList();

                foreach (var gridCommodities in commodities)
                {
                    if (user.PreferedWeightMeasurment.Equals("qn"))
                    {
                        gridCommodities.DispatchedQuantityMT *= 10;
                        gridCommodities.RequestedQuantityMT *= 10;
                    }
                }

                string str = Request["prev"];
                if (GetSelectedCommodities(str) != null)
                {
                    // TODO: revise this section
                    var allCommodities = GetSelectedCommodities(Request["prev"].ToString());
                    int count = -1;
                    foreach (var dispatchDetailViewModelComms in allCommodities)
                    {
                        if (dispatchDetailViewModelComms.Id == null)
                        {
                            //dispatchDetailViewModelComms.Id = count--;
                            dispatchDetailViewModelComms.DispatchDetailCounter = count--;
                            commodities.Add(dispatchDetailViewModelComms);
                        }
                        /**
                         * TODO the lines below are too nice to have but we need to look into the performance issue and 
                         * policies (i.e. editing should not be allowed ) may be only for quanitities 
                          */
                        else //replace the commodity read from the db by what's comming from the user
                        {
                            commodities.Remove(commodities.Find(p => p.Id == dispatchDetailViewModelComms.Id));
                            commodities.Add(dispatchDetailViewModelComms);
                        }
                    }
                }
            }
            else
            {
                string str = Request["prev"];
                if (GetSelectedCommodities(str) != null)
                {
                    commodities = GetSelectedCommodities(Request["prev"].ToString());
                    int count = -1;
                    foreach (var rdm in commodities)
                    {
                        if (rdm.Id != null)
                        {
                            rdm.DispatchDetailCounter = count--;
                        }
                    }
                }
            }
                
            ViewBag.Commodities = _commodityService.GetAllParents().Select(c => new Models.CommodityModel() { Id = c.CommodityID, Name = c.Name }).ToList();
            //TODO do we really need the line below 
            //PrepareCreate(1);
            return View(new GridModel(commodities));
        }

        private void PrepareCreate(int type)
        {
            var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
            ViewBag.Year = new SelectList(years, "Id", "Name"); 
            ViewBag.Month = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");


            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name");
            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name");

            ViewData["Commodities"] = _commodityService.GetAllParents().Select(c => new Models.CommodityModel() { Id = c.CommodityID, Name = c.Name }).ToList();
            ViewBag.Units = _unitService.GetAllUnit();
            if (type == 1)
            {
                PrepareFDPCreate();
            }
            else if (type == 2)
            {
                List<BLL.Hub> hub = _hubService.GetAllHub();
                hub.Remove(user.DefaultHub);
                ViewBag.ToHUBs = new SelectList(hub.Select(p => new { Name = string.Format("{0} - {1}", p.Name, p.HubOwner.Name), HubID = p.HubID }), "HubID", "Name");
            }

            List<Models.DispatchDetailModel> comms = new List<Models.DispatchDetailModel>();
            ViewBag.SelectedCommodities = comms;
            ViewBag.StackNumber = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        private void PrepareFDPCreate()
        {
            Models.AdminUnitModel unitModel = new AdminUnitModel();
            ViewBag.SelectedRegionId = new SelectList(_adminUnitService.GetRegions().Select(p => new{Id = p.AdminUnitID, Name = p.Name}), "Id", "Name");
            ViewBag.SelectedWoredaId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.FDPID = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.SelectedZoneId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
        }

        [HttpPost]
        public   ActionResult Create(Models.DispatchModel dispatchModel)
        {
            
            MembershipProvider membership = new MembershipProvider();
            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);

            List<Models.DispatchDetailModel> insertCommodities = new List<Models.DispatchDetailModel>();
            List<Models.DispatchDetailModel> updateCommodities = new List<Models.DispatchDetailModel>();
            List<Models.DispatchDetailModel> prevCommodities = new List<Models.DispatchDetailModel>();
            if (dispatchModel.JSONPrev != null)
            {
                 prevCommodities = GetSelectedCommodities(dispatchModel.JSONPrev);

                //Even though they are updated they are not saved so move them in to the inserted at the end of a succcessful submit
                int count = 0;
                foreach (var dispatchDetailAllViewModels in prevCommodities)
                {
                    if (dispatchDetailAllViewModels.Id != null)
                    {
                        count--;
                        dispatchDetailAllViewModels.DispatchDetailCounter = count;
                        insertCommodities.Add(dispatchDetailAllViewModels);
                    }
                    else
                    {
                        updateCommodities.Add(dispatchDetailAllViewModels);
                    }
                }

                ViewBag.ReceiveDetails = prevCommodities;
                ViewBag.SelectedCommodities = prevCommodities;
                dispatchModel.DispatchDetails = prevCommodities;
                bool isValid = ModelState.IsValid;

                //this check need's to be revisited
                if (prevCommodities.Count() == 0)
                {
                    ModelState.AddModelError("DispatchDetails", "Please add atleast one commodity to save this Dispatch");
                }
                string errorMessage = null;
                foreach (var dispatchDetailViewModel in prevCommodities)
                {
                    var validationContext = new ValidationContext(dispatchDetailViewModel, null, null);
                    IEnumerable<ValidationResult> validationResults = dispatchDetailViewModel.Validate(validationContext);
                    foreach (var v in validationResults)
                    {
                        errorMessage = string.Format("{0}, {1}", errorMessage, v.ErrorMessage);
                    }
                    Commodity comms = _commodityService.FindById(dispatchDetailViewModel.CommodityID);
                    CommodityType commType = _commodityTypeService.FindById(dispatchModel.CommodityTypeID);
                    if (dispatchModel.CommodityTypeID != comms.CommodityTypeID)
                        ModelState.AddModelError("DispatchDetails", comms.Name + " is not of type " + commType.Name);
                }
                if (errorMessage != null)
                {
                    ModelState.AddModelError("DispatchDetails", errorMessage);
                }
            }else
            {
                ModelState.AddModelError("DispatchDetails", "Please add atleast one commodity to save this Dispatch");
            }
            if (dispatchModel.Type != 1)
            {
                ModelState.Remove("FDPID");
                ModelState.Remove("RegionID");
                ModelState.Remove("WoredaID");
                ModelState.Remove("ZoneID");
                ModelState.Remove("BidNumber");
                dispatchModel.BidNumber = "00000";
                //NOT really needed 
                ModelState.Remove("Year");
                ModelState.Remove("Month");
            }
            else
            {
                ModelState.Remove("ToHubID");
            }
            
            if (ModelState.IsValid && user != null)
            {

                if (dispatchModel.ChangeStoreManPermanently != null && dispatchModel.ChangeStoreManPermanently == true)
                {
                    BLL.Store storeTobeChanged = _storeService.FindById(dispatchModel.StoreID);
                    if (storeTobeChanged != null && dispatchModel.ChangeStoreManPermanently == true)
                        storeTobeChanged.StoreManName = dispatchModel.DispatchedByStoreMan;
                }

                BLL.Dispatch dispatch = dispatchModel.GenerateDipatch();
                //if (dispatch.DispatchID == null )
                if(dispatchModel.DispatchID == null)
                {

                    dispatchModel.DispatchDetails = prevCommodities;
                    foreach (var gridCommodities in prevCommodities)
                    {
                        if (user.PreferedWeightMeasurment.Equals("qn"))
                        {
                            gridCommodities.DispatchedQuantityMT /= 10;
                            gridCommodities.RequestedQuantityMT /= 10;
                        }
                    }
                    //InsertDispatch(dispatchModel, user);
                    _transactionService.SaveDispatchTransaction(dispatchModel, user);
                }
                else
                {

                   // List<Models.DispatchDetailModel> insertCommodities = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
                    List<Models.DispatchDetailModel> deletedCommodities = GetSelectedCommodities(dispatchModel.JSONDeletedCommodities);
                   // List<Models.DispatchDetailModel> updateCommodities = GetSelectedCommodities(dispatchModel.JSONUpdatedCommodities);
                    dispatch.HubID = user.DefaultHub.HubID;
                    dispatch.Update(GenerateDispatchDetail(insertCommodities),
                        GenerateDispatchDetail(updateCommodities),
                        GenerateDispatchDetail(deletedCommodities));

                }
                
                return RedirectToAction("Index");
             }
            else
            {
                //List<Models.DispatchDetailModel> details = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
                //Session["SELCOM"] = details;

               // BLL.UserProfile user = BLL.UserProfile.GetUser(User.Identity.Name);
               PrepareCreate(dispatchModel.Type);

               if (dispatchModel.FDPID != null)
               {
                   PrepareFDPForEdit(dispatchModel.FDPID);
                   dispatchModel.WoredaID = _fdpService.FindById(dispatchModel.FDPID.Value).AdminUnitID;
               } //PrepareEdit(dispatchModel.GenerateDipatch(), user,dispatchModel.Type);
                return View(dispatchModel);
            
        }}
        //TODO remove this function later
        private void InsertDispatch(Models.DispatchModel dispatchModel, BLL.UserProfile user)
        {
            List<Models.DispatchDetailModel> commodities = GetSelectedCommodities(dispatchModel.JSONInsertedCommodities);
            dispatchModel.DispatchDetails = commodities;
            _transactionService.SaveDispatchTransaction(dispatchModel,user);
        }

       
       

        private static List<DispatchDetail> GenerateDispatchDetail(List<DispatchDetailModel> c)
        {
            
            if (c != null)
            {
                return (from m in c
                        select new BLL.DispatchDetail()
                    {
                        CommodityID = m.CommodityID,
                        Description = m.Description,
                        //DispatchDetailID = m.Id,
                        RequestedQuantityInMT = m.RequestedQuantityMT.Value,
                        //DispatchedQuantityInMT = c.DispatchedQuantityMT,
                        //DispatchedQuantityInUnit = c.DispatchedQuantity,
                        RequestedQunatityInUnit = m.RequestedQuantity.Value,
                        UnitID = m.Unit
                    }).ToList();
                }
            else
            {
                return new List<DispatchDetail>();
            }
        }

        public   ActionResult Months(int Year)
        {
            var months = from s in _periodService.GetMonths(Year)
                         select new { Name = s, Id = s };
            return Json(new SelectList(months,"Id","Name"), JsonRequestBehavior.AllowGet);
        }


        public   ActionResult JsonDispatch(string ginNo)
        {
            BLL.Dispatch dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            if (dispatch != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new EmptyResult();
            }
        }

        public   ActionResult _DispatchPartial(string ginNo, int type)
        {
            ViewBag.Units = _unitService.GetAllUnit();
            
            BLL.Dispatch dispatch = _dispatchService.GetDispatchByGIN(ginNo);
            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            if (dispatch != null)
            {
                type = dispatch.Type;//override the type by the data type coming from the DB(i.e. load the DB data by overriding the type)
                if (user.DefaultHub != null && user.DefaultHub.HubID == dispatch.HubID)
                {
                    PrepareEdit(dispatch, user, type);
                    return PartialView("DispatchPartial", Models.DispatchModel.GenerateDispatchModel(dispatch,repository));
                }
                else
                {
                    PrepareCreate(type);
                    ViewBag.Message = "The selected GIN Number doesn't exist on your default warehouse. Try changing your default warehouse.";
                    return PartialView("DispatchPartial", new Models.DispatchModel());
                }
            }
            else
            {
                PrepareCreate(type);
                return PartialView("DispatchPartial", new Models.DispatchModel());
            }
        }

        private void PrepareEdit(BLL.Dispatch dispatch, BLL.UserProfile user, int type)
        {
            var years = (from y in _periodService.GetYears()
                         select new { Name = y, Id = y }).ToList();
            var months = (from y in _periodService.GetMonths(dispatch.PeriodYear)
                         select new { Name = y, Id = y }).ToList();
            ViewBag.Year = new SelectList(years, "Id", "Name", dispatch.PeriodYear);
            ViewBag.Month = new SelectList(months, "Id", "Name", dispatch.PeriodMonth);
            ViewData["Units"] = _unitService.GetAllUnit().Select(p => new { Id = p.UnitID, Name = p.Name}).ToList();
            BLL.Transaction transaction = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
            

            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            if (type == 1)
            {
                PrepareFDPForEdit(dispatch.FDPID);
            }
            else if (type == 2)
            {
                BLL.Transaction tran = _dispatchService.GetDispatchTransaction(dispatch.DispatchID);
                //TODO I think there need's to be a check for this one 
                if(tran != null)
                    ViewBag.ToHUBs = new SelectList(_hubService.GetAllHub().Select(p => new {Name = string.Format("{0} - {1}",p.Name,p.HubOwner.Name), HubID = p.HubID}), "HubID", "Name", tran.Account.EntityID);
                else//this is deliberete change it later
                    ViewBag.ToHUBs = null;
                
            }

            if (transaction != null)
            {
                ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name", transaction.StoreID);
                ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", transaction.ProgramID);
                ViewBag.StackNumbers = new SelectList(transaction.Store.Stacks.Select(p => new { Name = p, Id = p }), "Id", "Name", transaction.Stack.Value);
                ViewData["Commodities"] = _commodityService.GetAllParents().Select(c => new Models.CommodityModel() { Id = c.CommodityID, Name = c.Name }).ToList();
                ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name",transaction.Commodity.CommodityTypeID);
            }
            else
            {
                ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID",
                                                 "Name"); //, transaction.StoreID);
                ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name");
                    //, transaction.ProgramID);
                //TODO i'm not so sure about the next line
                ViewBag.StackNumbers =
                    new SelectList(_storeService.GetAllStore().FirstOrDefault().Stacks.Select(p => new {Name = p, Id = p}), "Id",
                        "Name"); //, transaction.Stack.Value); )//transaction.Store.Stacks
                ViewData["Commodities"] =
                    _commodityService.GetAllParents().Select(
                        c => new Models.CommodityModel() {Id = c.CommodityID, Name = c.Name}).ToList();
                ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            }
            List<Models.DispatchDetailModel> comms = new List<Models.DispatchDetailModel>();
            ViewBag.SelectedCommodities = comms;
        }

        private void PrepareFDPForEdit(int? fdpid)
        {
            Models.AdminUnitModel unitModel = new Models.AdminUnitModel();
            BLL.FDP fdp;
            if(fdpid != null)
                fdp = _fdpService.FindById(fdpid.Value);
            else
                fdp = null;
            if (fdp != null)
            {
                unitModel.SelectedWoredaId = fdp.AdminUnitID;
                if (fdp.AdminUnit.ParentID != null) unitModel.SelectedZoneId = fdp.AdminUnit.ParentID.Value;

                unitModel.SelectedRegionId = _adminUnitService.GetRegionByZoneId(unitModel.SelectedZoneId);
                ViewBag.SelectedRegionId = new SelectList(_adminUnitService.GetRegions().Select(p => new { Id = p.AdminUnitID, Name = p.Name}).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedRegionId);
                ViewBag.SelectedZoneId = new SelectList(this.GetChildren(unitModel.SelectedRegionId).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedZoneId);
                ViewBag.SelectedWoredaId = new SelectList(this.GetChildren(unitModel.SelectedZoneId).OrderBy(o => o.Name), "Id", "Name", unitModel.SelectedWoredaId);
                ViewBag.FDPID = new SelectList(this.GetFdps(unitModel.SelectedWoredaId).OrderBy(o => o.Name), "Id", "Name", fdp.FDPID);
            }
            else
            {
                ViewBag.SelectedRegionId = new SelectList(unitModel.Regions, "Id", "Name");
                ViewBag.SelectedWoredaId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.FDPID = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.SelectedZoneId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            }
        }
        
        //
        // GET: /Dispatch/Edit/5

        public   ActionResult Edit(string id)
        {
            Dispatch dispatch = _dispatchService.FindById(Guid.Parse(id));
            //ViewBag.PeriodID = new SelectList(db.Periods, "PeriodID", "PeriodID", dispatch.PeriodID);
            ViewBag.StoreID = new SelectList(_storeService.GetAllStore(), "StoreID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "WarehouseID", "Name", dispatch.HubID);
            return View("Edit", dispatch);
        }

        //
        // POST: /Dispatch/Edit/5

        [HttpPost]
        public   ActionResult Edit(Dispatch dispatch)
        {
            if (ModelState.IsValid)
            {
                _dispatchService.EditDispatch(dispatch);
                return RedirectToAction("Index");
            }
            ViewBag.PeriodID = new SelectList(_periodService.GetAllPeriod(), "PeriodID", "PeriodID", _periodService.GetPeriod(dispatch.PeriodYear, dispatch.PeriodMonth).PeriodID);
            BLL.UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.StoreID = new SelectList(_storeService.GetStoreByHub(user.DefaultHub.HubID), "StoreID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name", dispatch.TransporterID);
            ViewBag.HubID = new SelectList(user.UserHubs, "HubID", "Name", dispatch.HubID);
            ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
            return View(dispatch);
        }

     

        
        public List<Models.AdminUnitItem> GetChildren(int parentId)
        {
            var units = from item in _adminUnitService.GetChildren(parentId)
                        select new Models.AdminUnitItem
                        {
                            Id = item.AdminUnitID,
                            Name = item.Name
                        };
            return units.ToList();
        }

        public List<Models.AdminUnitItem> GetFdps(int woredaId)
        {
            var fdps = from p in _fdpService.GetFDPsByWoreda(woredaId)
                       select new Models.AdminUnitItem() { Id = p.FDPID, Name = p.Name };
            return fdps.ToList();
        }

        private List<Models.DispatchDetailModel> GetSelectedCommodities(string jsonArray)
        {
            List<Models.DispatchDetailModel> commodities = null;
            if (!string.IsNullOrEmpty(jsonArray))
            {
                    commodities = JsonConvert.DeserializeObject<List<Models.DispatchDetailModel>>(jsonArray);      
            }
            return commodities;
        }

        public ActionResult IsProjectValid(string ProjectNumber)
        {
            var count = _projectCodeService.GetProjectCodeId(ProjectNumber);
            var result = (count > 0);
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult IsSIValid(string SINumber, int? FDPID)
        {
            bool result = false;
            if(FDPID != null)
            {
                result=  _shippingInstructionService.HasBalance(SINumber, FDPID.Value);
            }else
            {
                result = _shippingInstructionService.GetShipingInstructionId(SINumber) > 0;
            }
             
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult FDPBalance(string RequisitionNo, int FDPID)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            FDPBalance repositoryDispatchGetFDPBalance = repository.Dispatch.GetFDPBalance(FDPID, RequisitionNo);

            if (repositoryDispatchGetFDPBalance.CommodityTypeID == 1)
            {
                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "MT")
                {
                    repositoryDispatchGetFDPBalance.mesure = "MT";
                    repositoryDispatchGetFDPBalance.multiplier = 1;
                }
                else
                {
                    repositoryDispatchGetFDPBalance.mesure = "Qtl";
                    repositoryDispatchGetFDPBalance.multiplier = 10;
                }
            }
            else
            {
                repositoryDispatchGetFDPBalance.TotalAllocation *= 10;
                repositoryDispatchGetFDPBalance.CurrentBalance *= 10;
                //TODO fix the line below it's not corrcet for some cases
                
                repositoryDispatchGetFDPBalance.mesure = "Unit";
                repositoryDispatchGetFDPBalance.multiplier = 1;
            }

            return Json(repositoryDispatchGetFDPBalance, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AvailbaleCommodities(string SINumber)
        {
            return Json(_dispatchService.GetAvailableCommodities(SINumber, _userProfileService.GetUser(User.Identity.Name).DefaultHub.HubID).Select(p => new { Value = p.CommodityID, Text = p.Name })
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonRegionZones(string requisitionNumber)
        {
            List<DispatchAllocation> allocations = _dispatchAllocationService.GetAllocations(requisitionNumber);
            if(allocations.Count > 0)
            {
                var region = allocations.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID;
                var zone = allocations.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnitID;
                return Json(new {region, zone}, JsonRequestBehavior.AllowGet);
            }
            return Json( "" , JsonRequestBehavior.AllowGet);
        }
    }
}
