using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL.ViewModels.Dispatch;
using Telerik.Web.Mvc;

namespace DRMFSS.Web.Controllers.Allocations
{
    public class DispatchAllocationController : BaseController 
    {
        //
        // GET: /DispatchAllocation/

      
        public ActionResult Index()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            return View(repository.DispatchAllocation.GetAvailableRequisionNumbers(user.DefaultHub.HubID, true));
        }

        public ActionResult AllocationList()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            BLL.Hub hub = user.DefaultHub;
            var list = repository.DispatchAllocation.GetUncommitedAllocationsByHub(hub.HubID);
            return PartialView("AllocationList", list);
        }

        [HttpPost]
        public ActionResult CommitAllocation(string[] checkedRecords, int? SINumber, int? ProjectCode, string Sitext, string ProjectCodeText )
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if(checkedRecords != null && SINumber != -1 && ProjectCode != -1)
            {
                if(SINumber == 0 || ProjectCode == 0 )
                {
                    SINumber = repository.ShippingInstruction.GetSINumberIdWithCreate(Sitext).ShippingInstructionID;
                    ProjectCode = repository.ProjectCode.GetProjectCodeIdWIthCreate(ProjectCodeText).ProjectCodeID;
                }
                repository.DispatchAllocation.CommitDispatchAllocation(checkedRecords, SINumber.Value, user, ProjectCode.Value);
            }
            
            return RedirectToAction("Index","Dispatch");
        }

        public ActionResult GetAvailableSINumbers(int? CommodityID)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (CommodityID.HasValue)
            {
                var nums = from si in repository.DispatchAllocation.GetAvailableSINumbersWithUncommitedBalance(CommodityID.Value,user.DefaultHub.HubID)
                           select new { Name = si.Value, Id = si.ShippingInstructionID };

                return Json(new SelectList(nums, "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            return Json(new SelectList(Enumerable.Empty<SelectListItem>()),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommodities(string RequisitionNo)
        {
            if (!string.IsNullOrEmpty(RequisitionNo))
            {
                var commodities = from c in repository.DispatchAllocation.GetAvailableCommodities(RequisitionNo)
                           select new { Text = c.Name, Value = c.CommodityID };
                return Json(commodities, JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }

        public ActionResult SiBalances(string requisition)
        {
            // THIS only considers the first commodity
            // TODO: check if has to be that way.
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            Commodity commodity = repository.DispatchAllocation.GetAvailableCommodities(requisition).FirstOrDefault();
            List<SIBalance> sis = repository.DispatchAllocation.GetUncommitedSIBalance(UserProfile.DefaultHub.HubID, commodity.CommodityID,user.PreferedWeightMeasurment);
            ViewBag.UnitType = commodity.CommodityTypeID;
            return PartialView("SIBalance", sis);
        }

        public ActionResult GetBalance(int? siNumber, int? commodityId, string siNumberText)
        {
            if (siNumber.HasValue && commodityId.HasValue)
            {
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                List<SIBalance> si = (from v in repository.DispatchAllocation.GetUncommitedSIBalance(
                                              UserProfile.DefaultHub.HubID,
                                              commodityId.Value,user.PreferedWeightMeasurment)
                                      select v).ToList();
                
               
                SIBalance sis = new SIBalance();
                    if(siNumber.Value == 0 )
                        sis = si.FirstOrDefault(v1 => v1.SINumber == siNumberText); 
                    else
                        sis = si.FirstOrDefault(v1 => v1.SINumberID == siNumber.Value);
                    
               
                decimal balance = sis.Dispatchable;// +ReaminingExpectedReceipts;
                return Json(balance, JsonRequestBehavior.AllowGet);
            }
            return Json(new EmptyResult());
        }

        public ActionResult GetAllocations(string RquisitionNo, int? CommodityID, bool Uncommited)
        {
            ViewBag.req = RquisitionNo;
            ViewBag.Com = CommodityID;
            ViewBag.Uncommited = Uncommited;
            var list = new List<BLL.DispatchAllocation>();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (!string.IsNullOrEmpty(RquisitionNo) && CommodityID.HasValue)
            {
                list = repository.DispatchAllocation.GetAllocations(RquisitionNo, CommodityID.Value, user.DefaultHub.HubID, Uncommited, user.PreferedWeightMeasurment);
            }
            else if (!string.IsNullOrEmpty(RquisitionNo))
            {
                list = repository.DispatchAllocation.GetAllocations(RquisitionNo, user.DefaultHub.HubID, Uncommited);
            }

            List<DispatchAllocationViewModelDto> FDPAllocations = new List<DispatchAllocationViewModelDto>();
            foreach (var dispatchAllocation in list)
            {
                var DAVMD = new DispatchAllocationViewModelDto();
                string preferedWeightMeasurment = user.PreferedWeightMeasurment.ToUpperInvariant();
                //if (preferedWeightMeasurment == "MT" && dispatchAllocation.Commodity.CommodityTypeID == 1) //only for food
                //{
                //    DAVMD.Amount = dispatchAllocation.Amount / 10;
                //    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount / 10;
                //    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals / 10;
                //}
                //else
                {
                    DAVMD.Amount = dispatchAllocation.Amount;
                    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount;
                    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals;
                }
                DAVMD.DispatchAllocationID = dispatchAllocation.DispatchAllocationID;
                DAVMD.CommodityName = dispatchAllocation.Commodity.Name;
                DAVMD.CommodityID = dispatchAllocation.CommodityID;
                DAVMD.RequisitionNo = dispatchAllocation.RequisitionNo;
                DAVMD.BidRefNo = dispatchAllocation.BidRefNo;
                DAVMD.ProjectCodeID = dispatchAllocation.ProjectCodeID;
                DAVMD.ShippingInstructionID = dispatchAllocation.ShippingInstructionID;

                DAVMD.Region = dispatchAllocation.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                DAVMD.Zone = dispatchAllocation.FDP.AdminUnit.AdminUnit2.Name;
                DAVMD.Woreda = dispatchAllocation.FDP.AdminUnit.Name;
                DAVMD.FDPName = dispatchAllocation.FDP.Name;
                DAVMD.TransporterName = dispatchAllocation.Transporter.Name;
                DAVMD.IsClosed = dispatchAllocation.IsClosed;


                DAVMD.AmountInUnit = DAVMD.Amount;
                DAVMD.DispatchedAmountInUnit = dispatchAllocation.DispatchedAmountInUnit;
                DAVMD.RemainingQuantityInUnit = dispatchAllocation.RemainingQuantityInUnit;

                FDPAllocations.Add(DAVMD);

            }

            return PartialView("AllocationList", FDPAllocations);

        }


        [GridAction]
        public ActionResult GetAllocationsGrid(string RquisitionNo, int? CommodityID, bool? Uncommited)
        {
            bool commitStatus = true;
            if (Uncommited.HasValue) commitStatus = Uncommited.Value;
            var list = new List<BLL.DispatchAllocation>();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (!string.IsNullOrEmpty(RquisitionNo) && CommodityID.HasValue)
            {
                list = repository.DispatchAllocation.GetAllocations(RquisitionNo, CommodityID.Value, user.DefaultHub.HubID, commitStatus, user.PreferedWeightMeasurment);
            }
            else if (!string.IsNullOrEmpty(RquisitionNo))
            {
                list = repository.DispatchAllocation.GetAllocations(RquisitionNo, user.DefaultHub.HubID, commitStatus);
            }
            List<DispatchAllocationViewModelDto> FDPAllocations = new List<DispatchAllocationViewModelDto>();
            foreach (var dispatchAllocation in list)
            {
                var DAVMD = new DispatchAllocationViewModelDto();
                string preferedWeightMeasurment = user.PreferedWeightMeasurment.ToUpperInvariant();
                //if (preferedWeightMeasurment == "MT" && dispatchAllocation.Commodity.CommodityTypeID == 1) //only for food
                //{
                //    DAVMD.Amount = dispatchAllocation.Amount / 10;
                //    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount / 10;
                //    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals / 10;
                //}
                //else
                {
                    DAVMD.Amount = dispatchAllocation.Amount;
                    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount;
                    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals;
                }
                DAVMD.DispatchAllocationID = dispatchAllocation.DispatchAllocationID;
                DAVMD.CommodityName = dispatchAllocation.Commodity.Name;
                DAVMD.CommodityID = dispatchAllocation.CommodityID;
                DAVMD.RequisitionNo = dispatchAllocation.RequisitionNo;
                DAVMD.BidRefNo = dispatchAllocation.BidRefNo;
                DAVMD.ProjectCodeID = dispatchAllocation.ProjectCodeID;
                DAVMD.ShippingInstructionID = dispatchAllocation.ShippingInstructionID;

                DAVMD.Region = dispatchAllocation.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                DAVMD.Zone = dispatchAllocation.FDP.AdminUnit.AdminUnit2.Name;
                DAVMD.Woreda = dispatchAllocation.FDP.AdminUnit.Name;
                DAVMD.FDPName = dispatchAllocation.FDP.Name;
                DAVMD.TransporterName = dispatchAllocation.Transporter.Name;
                DAVMD.IsClosed = dispatchAllocation.IsClosed;


                DAVMD.AmountInUnit = DAVMD.Amount;
                DAVMD.DispatchedAmountInUnit = dispatchAllocation.DispatchedAmountInUnit;
                DAVMD.RemainingQuantityInUnit = dispatchAllocation.RemainingQuantityInUnit;

                FDPAllocations.Add(DAVMD);

            }

            return View(new GridModel(FDPAllocations));

        }

        public ActionResult GetSIBalances()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            var list = repository.DispatchAllocation.GetSIBalances(user.DefaultHub.HubID);
            return PartialView("SIBalance", list);
        }

        public ActionResult AvailableRequistions(bool UnCommited)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            return Json(repository.DispatchAllocation.GetAvailableRequisionNumbers(user.DefaultHub.HubID, UnCommited), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.CommodityTypes = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name");
            ViewBag.Commodities = new SelectList(repository.Commodity.GetAllParents(), "CommodityID", "Name");
            ViewBag.Donors = new SelectList(repository.Donor.GetAll(), "DonorID", "Name");
            ViewBag.Regions = new SelectList(repository.AdminUnit.GetRegions(), "AdminUnitID", "Name");
            ViewBag.Zones = new SelectList(Enumerable.Empty<SelectListItem>(), "AdminUnitID", "Name");
            ViewBag.Woredas = new SelectList(Enumerable.Empty<SelectListItem>(), "AdminUnitID", "Name");
            ViewBag.FDPS = new SelectList(Enumerable.Empty<SelectListItem>(), "FDPID", "Name");
            ViewBag.Years = new SelectList(repository.Period.GetYears().Select(y => new { Name = y, Id = y }), "Id", "Name");
            ViewBag.Months = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.Transporters = new SelectList(repository.Transporter.GetAll(), "TransporterID", "Name");
            ViewBag.Programs = new SelectList(repository.Program.GetAll(), "ProgramID", "Name");
            ViewBag.Units = new SelectList(repository.Unit.GetAll(), "UnitID", "Name");
            return PartialView("Create", new BLL.ViewModels.DispatchAllocationViewModel());
        }

        [HttpPost]
        public ActionResult Create(BLL.ViewModels.DispatchAllocationViewModel allocation)
        {
            if (ModelState.IsValid)
            {
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                BLL.DispatchAllocation alloc = GetAllocationModel(allocation);
                alloc.HubID = user.DefaultHub.HubID;
                repository.DispatchAllocation.Add(alloc);
                if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("Index");
            }
            PrepareEdit(allocation);
            return PartialView("Create",allocation);
        }


        public ActionResult Edit(string id)
        {
            BLL.DispatchAllocation allocation = repository.DispatchAllocation.FindById(Guid.Parse(id));
            BLL.ViewModels.DispatchAllocationViewModel alloc = GetAllocationModel(allocation);
            alloc.CommodityTypeID = allocation.Commodity.CommodityTypeID;
            PrepareEdit(alloc);
            return PartialView("Edit", alloc);
            
            
        }

        private void PrepareCreate()
        {
            ViewBag.CommodityTypes = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name");
            ViewBag.Commodities = new SelectList(repository.Commodity.GetAllParents(), "CommodityID", "Name");
            ViewBag.Donors = new SelectList(repository.Donor.GetAll(), "DonorID", "Name");
            ViewBag.Regions = new SelectList(repository.AdminUnit.GetRegions(), "AdminUnitID", "Name");
            ViewBag.Zones = new SelectList(Enumerable.Empty<SelectListItem>(), "AdminUnitID", "Name");
            ViewBag.Woredas = new SelectList(Enumerable.Empty<SelectListItem>(), "AdminUnitID", "Name");
            ViewBag.FDPS = new SelectList(Enumerable.Empty<SelectListItem>(), "FDPID", "Name");
            ViewBag.Years = new SelectList(repository.Period.GetYears().Select(y => new { Name = y, Id = y }), "Id", "Name");
            ViewBag.Months = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            ViewBag.Transporters = new SelectList(repository.Transporter.GetAll(), "TransporterID", "Name");
            ViewBag.Programs = new SelectList(repository.Program.GetAll(), "ProgramID", "Name");
            ViewBag.Units = new SelectList(repository.Unit.GetAll(), "UnitID", "Name");
        }

        [HttpPost]
        public ActionResult Edit(BLL.ViewModels.DispatchAllocationViewModel allocation)
        {
            if (ModelState.IsValid)
            {
                BLL.DispatchAllocation alloc = GetAllocationModel(allocation);
                repository.DispatchAllocation.SaveChanges(alloc);
                if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("Index");
                //return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            PrepareEdit(allocation);
            return PartialView(allocation);
        }

        private void PrepareEdit(BLL.ViewModels.DispatchAllocationViewModel allocation)
        {
            ViewBag.Commodities = new SelectList(repository.Commodity.GetAllParents(), "CommodityID", "Name", allocation.CommodityID);

            ViewBag.CommodityTypes = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name",allocation.CommodityTypeID);

            ViewBag.Donors = new SelectList(repository.Donor.GetAll(), "DonorID", "Name", allocation.DonorID);
            
            ViewBag.Years = new SelectList(repository.Period.GetYears().Select(y => new { Name = y, Id = y }), "Id", "Name", allocation.Year);
            if (allocation.Year.HasValue)
            {
                ViewBag.Months = new SelectList(repository.Period.GetMonths(allocation.Year.Value).Select(p => new{Id = p, Name = p }), "Id", "Name", allocation.Month);
            }
            else
            {
                ViewBag.Months = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            }
            ViewBag.Transporters = new SelectList(repository.Transporter.GetAll(), "TransporterID", "Name",allocation.TransporterID);
            ViewBag.Programs = new SelectList(repository.Program.GetAll(), "ProgramID", "Name",allocation.ProgramID);
            ViewBag.Units = new SelectList(repository.Unit.GetAll(), "UnitID", "Name",allocation.Unit);

           // TODO we can use the line below for debugging and server side validation
            PrepareFDPForEdit(allocation.FDPID);

        }

        private void PrepareFDPForEdit(int? fdpid)
        {

            Models.AdminUnitModel unitModel = new Models.AdminUnitModel();
            BLL.FDP fdp;
            if (fdpid != null)
                fdp = repository.FDP.FindById(fdpid.Value);
            else
                fdp = null;
            if (fdp != null)
            {
                unitModel.SelectedWoredaId = fdp.AdminUnitID;
                if (fdp.AdminUnit.ParentID != null) unitModel.SelectedZoneId = fdp.AdminUnit.ParentID.Value;

                unitModel.SelectedRegionId = repository.AdminUnit.GetRegionByZoneId(unitModel.SelectedZoneId);
                ViewBag.Regions =
                    new SelectList(
                        repository.AdminUnit.GetRegions().Select(p => new {Id = p.AdminUnitID, Name = p.Name}).OrderBy(
                            o => o.Name), "Id", "Name", unitModel.SelectedRegionId);
                ViewBag.Zones =
                    new SelectList(this.GetChildren(unitModel.SelectedRegionId).OrderBy(o => o.Name), "Id", "Name",
                                   unitModel.SelectedZoneId);
                ViewBag.Woredas =
                    new SelectList(this.GetChildren(unitModel.SelectedZoneId).OrderBy(o => o.Name), "Id", "Name",
                                   unitModel.SelectedWoredaId);
                ViewBag.FDPS = new SelectList(this.GetFdps(unitModel.SelectedWoredaId).OrderBy(o => o.Name), "Id",
                                               "Name", fdp.FDPID);
            }
            else
            {
                ViewBag.SelectedRegionId = new SelectList(unitModel.Regions, "Id", "Name");
                ViewBag.SelectedWoredaId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.FDPID = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
                ViewBag.SelectedZoneId = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Name");
            }

        }

        public List<Models.AdminUnitItem> GetFdps(int woredaId)
        {
            var fdps = from p in repository.FDP.GetFDPsByWoreda(woredaId)
                       select new Models.AdminUnitItem() { Id = p.FDPID, Name = p.Name };
            return fdps.ToList();
        }

        public List<Models.AdminUnitItem> GetChildren(int parentId)
        {
            var units = from item in repository.AdminUnit.GetChildren(parentId)
                        select new Models.AdminUnitItem
                        {
                            Id = item.AdminUnitID,
                            Name = item.Name
                        };
            return units.ToList();
        }

        public ActionResult SelectionHeader( string requisition)
        {
            DispatchAllocation dispatchAllocation =
                repository.DispatchAllocation.GetAllocations(requisition).FirstOrDefault();
           return PartialView(dispatchAllocation);
        }


        private BLL.ViewModels.DispatchAllocationViewModel GetAllocationModel(BLL.DispatchAllocation dispatch)
        {
            BLL.ViewModels.DispatchAllocationViewModel model = new BLL.ViewModels.DispatchAllocationViewModel(dispatch.FDPID, repository);
            model.Amount = dispatch.Amount;
            model.Beneficiery = dispatch.Beneficiery;
            model.BidRefNo = dispatch.BidRefNo;
            model.CommodityID = dispatch.CommodityID;
            model.DispatchAllocationID = dispatch.DispatchAllocationID;
            model.DonorID = dispatch.DonorID;
            model.FDPID = dispatch.FDPID;
            model.HubID = dispatch.HubID;
            model.Month = dispatch.Month;
            model.PartitionID = dispatch.PartitionID;
            model.ProgramID = dispatch.ProgramID;
            model.ProjectCodeID = dispatch.ProjectCodeID;
            model.RequisitionNo = dispatch.RequisitionNo;
            model.Round = dispatch.Round;
            model.ShippingInstructionID = dispatch.ShippingInstructionID;
            model.TransporterID = dispatch.TransporterID;
            model.Unit = dispatch.Unit;
            model.Year = dispatch.Year;
            model.CommodityTypeID = dispatch.Commodity.CommodityTypeID;
            return model;
        }

        /// <summary>
        /// Gets the allocation model.
        /// </summary>
        /// <param name="dispatch">The dispatch.</param>
        /// <returns></returns>
        private BLL.DispatchAllocation GetAllocationModel(BLL.ViewModels.DispatchAllocationViewModel dispatch)
        {
            BLL.DispatchAllocation model = new BLL.DispatchAllocation();
            model.Amount = dispatch.Amount;
            model.Beneficiery = dispatch.Beneficiery;
            model.BidRefNo = dispatch.BidRefNo;
            model.CommodityID = dispatch.CommodityID;
            if(dispatch.DispatchAllocationID.HasValue)
            {
                model.DispatchAllocationID = dispatch.DispatchAllocationID.Value;    
            }
            
            model.DonorID = dispatch.DonorID;
            model.FDPID = dispatch.FDPID;
            model.HubID = dispatch.HubID;
            model.Month = dispatch.Month;
            model.PartitionID = dispatch.PartitionID;
            model.ProgramID = dispatch.ProgramID;
            model.ProjectCodeID = dispatch.ProjectCodeID;
            model.RequisitionNo = dispatch.RequisitionNo;
            model.Round = dispatch.Round;
            model.ShippingInstructionID = dispatch.ShippingInstructionID;
            model.TransporterID = dispatch.TransporterID;
            model.Unit = dispatch.Unit;
            model.Year = dispatch.Year;
            return model;
        }

        /// <summary>
        /// Allocate dispatch To the other owners.
        /// </summary>
        /// <returns></returns>
        public ActionResult ToOtherOwners()
        {
            var model = repository.OtherDispatchAllocation.GetAllToOtherOwnerHubs(repository.UserProfile.GetUser(User.Identity.Name));
            return View(model);
            
        }

        /// <summary>
        /// Allocate dispatch To hubs of the same owner ( Transfer and Swap) 
        /// </summary>
        /// <returns></returns>
        public ActionResult ToHubs()
        {
            var model = repository.OtherDispatchAllocation.GetAllToCurrentOwnerHubs(repository.UserProfile.GetUser(User.Identity.Name));
            return View(model);
        }

        public ActionResult CreateTransfer()
        {
            var model = new OtherDispatchAllocationViewModel();
            model.InitTransfer( repository.UserProfile.GetUser(User.Identity.Name), repository );
            return PartialView("EditTransfer", model);
        }

        public ActionResult EditTransfer(string id)
        {
            var model = repository.OtherDispatchAllocation.GetViewModelByID( Guid.Parse(id));
            model.InitTransfer(repository.UserProfile.GetUser(User.Identity.Name), repository);
            return PartialView("EditTransfer", model);
        }

        // Only do the save if this has been a post.
        [HttpPost]
        public ActionResult SaveTransfer(OtherDispatchAllocationViewModel model)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            model.FromHubID = user.DefaultHub.HubID;
            if (ModelState.IsValid)
            {
                repository.OtherDispatchAllocation.Save(model);
                if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("ToHubs");
                //return RedirectToAction("ToHubs");
            }
            else
            {
                model.InitTransfer(repository.UserProfile.GetUser(User.Identity.Name), repository);
                return PartialView("EditTransfer", model);
            }
        }


        public ActionResult CreateLoan()
        {
            var model = new OtherDispatchAllocationViewModel();
            model.InitLoan(repository.UserProfile.GetUser(User.Identity.Name), repository);
            return PartialView("EditLoans", model);
        }

        public ActionResult EditLoan(string id)
        {
            var model = repository.OtherDispatchAllocation.GetViewModelByID( Guid.Parse(id));
            model.InitLoan(repository.UserProfile.GetUser(User.Identity.Name), repository);
            return PartialView("EditLoans", model);
        }

        // Only do the save if this has been a post.
        [HttpPost]
        public ActionResult SaveLoan(OtherDispatchAllocationViewModel model)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            model.FromHubID = user.DefaultHub.HubID;
            if (ModelState.IsValid)
            {
                repository.OtherDispatchAllocation.Save(model);
                if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("ToOtherOwners");
                //return RedirectToAction("ToOtherOwners");
            }
            else
            {
                model.InitLoan(repository.UserProfile.GetUser(User.Identity.Name), repository);
                return PartialView("EditLoans", model);
            }
        }

        // remote validations
        public ActionResult IsProjectValid(string ProjectCode)
        {
            var count = repository.ProjectCode.GetProjectCodeId(ProjectCode);
            var result = (count > 0);
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult IsSIValid(string ShippingInstruction)
        {
            bool result = false;
            result = repository.ShippingInstruction.GetShipingInstructionId(ShippingInstruction) > 0;
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult Close(string id)
        {
            var closeAllocation = repository.DispatchAllocation.FindById(Guid.Parse(id));
            return PartialView("Close", closeAllocation);
        }

        [HttpPost, ActionName("Close")]
        public ActionResult CloseConfirmed(string id)
        {
            var closeAllocation = repository.DispatchAllocation.FindById(Guid.Parse(id));
            if (closeAllocation != null)
            {
                repository.DispatchAllocation.CloseById(Guid.Parse(id));
                return Json(new { gridNum = 1 }, JsonRequestBehavior.AllowGet);
                //if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                //else return RedirectToAction("Index");
            }
            return this.Close(id);
        }

        public ActionResult OtherClose(string id)
        {
            var closeAllocation = repository.OtherDispatchAllocation.FindById(Guid.Parse(id));
            return PartialView("CloseOther", closeAllocation);
        }

        [HttpPost, ActionName("OtherClose")]
        public ActionResult OtherCloseConfirmed(string id)
        {
            var closeAllocation = repository.OtherDispatchAllocation.FindById(Guid.Parse(id));
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (closeAllocation != null)
            {
                repository.OtherDispatchAllocation.CloseById(Guid.Parse(id));
                int? gridNum = null;
                if (closeAllocation.Hub1.HubOwnerID == user.DefaultHub.HubOwnerID)
                {
                    gridNum = 3;
                }
                else
                {
                    gridNum = 2;
                }        

                return Json(new { gridNum }, JsonRequestBehavior.AllowGet);
                //if (this.Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                //else return RedirectToAction("Index");
            }
            return this.Close(id);
        }



    }
}
