using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels;
using Telerik.Web.Mvc;
using DRMFSS.Web.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DRMFSS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public partial class ReceiveController : BaseController
    {
       
        public ActionResult SINotUnique(string SINUmber, int CommoditySourceID)
        {

            //check allocation and gc for the same record
            var fromGc = repository.GiftCertificate.FindBySINumber(SINUmber);
            var fromRall =
                repository.ReceiptAllocation.FindBySINumber(SINUmber).FirstOrDefault(p => p.CommoditySourceID == BLL.CommoditySource.Constants.DONATION);


            if (CommoditySourceID == BLL.CommoditySource.Constants.LOCALPURCHASE)
            {
                return Json
                    ((fromGc == null) && (fromRall == null), JsonRequestBehavior.AllowGet);
            }
            //just incase the user is bad
            var fromRallt =
                repository.ReceiptAllocation.FindBySINumber(SINUmber).FirstOrDefault(p => p.CommoditySourceID == BLL.CommoditySource.Constants.LOCALPURCHASE);

            if (CommoditySourceID == BLL.CommoditySource.Constants.DONATION)
            {
                return Json
                    ((fromRallt == null), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Receive/

        //NOTE: this doesn't take editing/Recipt with same Receiveid same into consideration 
        public ActionResult NotFoundSI(String SINumber, int CommodityID)
        {
            return
                Json(repository.ReceiptAllocation.GetAvailableCommodities(SINumber,repository.UserProfile.GetUser(User.Identity.Name).DefaultHub.HubID).Select(
                    p => p.CommodityID == CommodityID).Any(), JsonRequestBehavior.AllowGet );

        }

        public ActionResult AllocationList(int type)
        {
            //TODO perform type specification here
            //Just return an empty list and bind it later
            List<ReceiptAllocation> list = new List<ReceiptAllocation>();
            ViewBag.CommoditySourceType = type;
            ViewBag.CommodityTypes = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name", 1); //make the inital binding a food type
            return PartialView("Allocations", list);
        }

        [GridAction]
        public ActionResult AllocationListGrid(int type, bool? closedToo, int? CommodityType)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            List<ReceiptAllocation> list = repository.ReceiptAllocation.GetUnclosedAllocationsDetached(user.DefaultHub.HubID, type, closedToo, user.PreferedWeightMeasurment, CommodityType);
            return View(new GridModel(list));
        }


        /// <summary>
        /// Checks if the GRN Number is not Unique
        /// </summary>
        /// <param name="grn">The GRN.</param>
        /// <param name="receiveId">The receive id.</param>
        /// <returns></returns>
        public virtual ActionResult NotUnique(string grn, string receiveId)
        {

            BLL.Receive receive = BLL.Receive.GetReceiveByGRN(grn);
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            Guid guidParse = Guid.Empty;
            if (Guid.TryParse(receiveId, out guidParse))
            {
                guidParse = Guid.Parse(receiveId);
            }


            if (receive == null || (receive.ReceiveID != Guid.Empty) && (receive.ReceiveID == guidParse))// new one or edit no problem 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            } else {

                if (receive.HubID == user.DefaultHub.HubID)
                {
                    return Json(string.Format("{0} is invalid, there is an existing record with the same GRN", grn),
                        JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(string.Format("{0} is invalid, there is an existing record with the same GRN at another Warehouse", grn),
                    JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// Shows a list of receive transactions.
        /// </summary>
        /// <returns></returns>
        
        public virtual ActionResult Index()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            List<BLL.Receive> receives = repository.Receive.ByHubId(user.DefaultHub.HubID);
         
            return View(receives);
        }

        public virtual ActionResult Log()
        {
            
            List<BLL.Receive> receives = repository.Receive.ByHubId(GetCurrentUserProfile().DefaultHub.HubID);
            return View(receives);
        }
        
        [GridAction]
        public ActionResult ReceiveListGrid(string ReceiptAllocationID)
        {
            BLL.UserProfile user =  repository.UserProfile.GetUser(User.Identity.Name);
           //TODO cascade using allocation id
            List<ReceiveViewModelDto> receives = repository.Receive.ByHubIdAndAllocationIDetached(user.DefaultHub.HubID, Guid.Parse(ReceiptAllocationID));
            return View(new GridModel(receives));
        }

        [GridAction]
        public ActionResult ReceiveListGridListGrid(string ReceiveID)
        {
            BLL.UserProfile user =  repository.UserProfile.GetUser(User.Identity.Name);
            //(user.DefaultHub.HubID)
            List<ReceiveDetailViewModelDto> receiveDetails = repository.ReceiveDetail.ByReceiveIDetached(Guid.Parse(ReceiveID),user.PreferedWeightMeasurment);
            return View(new GridModel(receiveDetails));
        }

        //
        // GET: /Receive/Create

        public virtual ActionResult Create(string receiveId)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            if (receiveId != "" && receiveId != null)
            {
                BLL.Receive receive = repository.Receive.FindById(Guid.Parse(receiveId));

                if (receive != null && (receive.HubID == user.DefaultHub.HubID))
                {
                    return View("Create", ReceiveViewModel.GenerateReceiveModel(receive, user));
                }
                else if (receive != null && (receive.HubID != user.DefaultHub.HubID))
                {
                    ViewBag.Message =
                        string.Format(
                            "The selected GRN Number {0} doesn't exist in your default warehouse. Try changing your default warehouse.",
                            receive.GRN);
                }
            }

            // Hack 
            ViewBag.Stacks = new SelectList(Enumerable.Empty<SelectListItem>());
            List<Models.ReceiveDetailViewModel> receiveCommodities = new List<Models.ReceiveDetailViewModel>();
            ViewBag.ReceiveCommodities = receiveCommodities;

            var receiveViewModel = new Models.ReceiveViewModel(repository, user);
            if (Request["type"] != null)
            {
                receiveViewModel.CommoditySourceID = Convert.ToInt32(Request["type"]);
            }
            else
            {
                receiveViewModel.CommoditySourceID = 1;
            }


            //receiveViewModel.SINumber = Request["SINumber"];
            if (Request["ReceiptAllocationID"] != null)
            {


                Guid receiptAllocationID = Guid.Parse(Request["ReceiptAllocationID"]);
                //Added (p.HubID == user.DefaultHub.HubID) check to load allocation only for the current user's defalt Hub
                //get populate using allocation by current hub,comsource and and si number,and not closed
                //TODO use the merge function to have swap,repay,..and others

                //var rAllocation = repository.ReceiptAllocation.GetAllByTypeMerged(receiveViewModel.CommoditySourceID)
                //                  .FirstOrDefault(p => p.SINumber == receiveViewModel.SINumber && p.HubID == user.DefaultHub.HubID
                //                  && p.IsClosed == false);

                //TODO we can load all the sub-commodites here
                //var rAllocation = repository.ReceiptAllocation.FindBySINumber(receiveViewModel.SINumber).FirstOrDefault(
                //    p => p.CommoditySourceID == receiveViewModel.CommoditySourceID && p.HubID == user.DefaultHub.HubID && p.IsClosed == false);
                var rAllocation = repository.ReceiptAllocation.FindById(receiptAllocationID);

                if (rAllocation != null)
                {

                    //Only for loading Waybill no
                    BLL.GiftCertificate gCertificate = repository.GiftCertificate.FindBySINumber(rAllocation.SINumber);
                    if (gCertificate != null)
                    {
                        var giftCertificateDetail = gCertificate.GiftCertificateDetails.FirstOrDefault();
                        if (giftCertificateDetail != null)
                        {
                            receiveViewModel.WayBillNo = giftCertificateDetail.BillOfLoading;

                        }
                        receiveViewModel.PortName = gCertificate.PortName;
                        receiveViewModel.VesselName = gCertificate.Vessel;
                    }

                    if (rAllocation.HubID == user.DefaultHub.HubID)
                    {
                        //if the allocation is for the current hub allocation 
                        receiveViewModel.ReceiptAllocationID = rAllocation.ReceiptAllocationID;
                        receiveViewModel.SINumber = rAllocation.SINumber;
                        receiveViewModel.ProjectNumber = rAllocation.ProjectNumber;
                        receiveViewModel.ProgramID = rAllocation.ProgramID;
                        receiveViewModel.CommodityTypeID = rAllocation.Commodity.CommodityTypeID;

                        receiveViewModel.ResponsibleDonorID = rAllocation.DonorID;
                        //receiveViewModel.CommoditySourceID = rAllocation.DonorID;
                        receiveViewModel.SupplierName = rAllocation.SupplierName;
                        receiveViewModel.SourceHubID = rAllocation.SourceHubID;
                        receiveViewModel.PurchaseOrder = rAllocation.PurchaseOrder;

                        if (rAllocation.Commodity.ParentID != null)
                        {
                            //TODO if we were to load multiple commoditites don't forget to derecement the allocation id in the neative direction
                            receiveCommodities.Add(new ReceiveDetailViewModel()
                                                       {ReceiveDetailID = null, ReceiveDetailCounter = -1, CommodityID = rAllocation.CommodityID});
                            receiveViewModel.JSONPrev = JsonConvert.SerializeObject(receiveCommodities);
                        }
                    }
                    else
                    {
                        ViewBag.Message =
                            string.Format(
                                "The selected Receipt Allocation {0} doesn't exist in your default warehouse. Try changing your default warehouse.",
                                rAllocation.ReceiptAllocationID);
                    }
                }

            }
            return View("Create", receiveViewModel);
        }

        public virtual ActionResult _ReceivePartial(string grnNo)
        {

            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            BLL.Receive receive = BLL.Receive.GetReceiveByGRN(grnNo);
            if (receive != null && (receive.HubID == user.DefaultHub.HubID))
            {
                return PartialView("_ReceivePartial", ReceiveViewModel.GenerateReceiveModel(receive, user));
            }
            else if (receive != null && (receive.HubID != user.DefaultHub.HubID))
            {
                ViewBag.Message = string.Format("The selected GRN Number {0} doesn't exist in your default warehouse. Try changing your default warehouse.",grnNo);
            }
            

            ViewBag.Stacks = new SelectList(Enumerable.Empty<SelectListItem>());
            List<Models.ReceiveDetailViewModel> ReceiveCommodities = new List<Models.ReceiveDetailViewModel>();
            ViewBag.ReceiveCommodities = ReceiveCommodities;
            var viewmode = new Models.ReceiveViewModel(repository, user);
           // viewmode.GRN = grnNo;
            return PartialView("_ReceivePartial", viewmode);
        }


        private List<Models.ReceiveDetailViewModel> GetSelectedCommodities(string jsonArray)
        {
            List<Models.ReceiveDetailViewModel> commodities = new List<Models.ReceiveDetailViewModel>();
            if (!string.IsNullOrEmpty(jsonArray))
            {
                commodities = JsonConvert.DeserializeObject<List<Models.ReceiveDetailViewModel>>(jsonArray);
            }
            return commodities;
        }



        // POST: /Receive/Create

        /// <summary>
        /// Creates the specified receive record from the receive model .
        /// </summary>
        /// <param name="receiveModels">The receive models.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Create(ReceiveViewModel receiveModels)
        {

            MembershipProvider membership = new MembershipProvider();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            List<Models.ReceiveDetailViewModel> insertCommodities = new List<Models.ReceiveDetailViewModel>();
            List<Models.ReceiveDetailViewModel> updateCommodities = new List<Models.ReceiveDetailViewModel>();
            List<Models.ReceiveDetailViewModel> prevCommodities = new List<Models.ReceiveDetailViewModel>();
            if (receiveModels.JSONPrev != null)
            {
                prevCommodities = GetSelectedCommodities(receiveModels.JSONPrev);

                //Even though they are updated they are not saved so move them in to the inserted at the end of a succcessful submit
                int count = 0;
                foreach (var receiveDetailAllViewModels in prevCommodities)
                {
                    if (receiveDetailAllViewModels.ReceiveDetailID == null)
                    {
                        count--;
                        receiveDetailAllViewModels.ReceiveDetailCounter = count;
                        insertCommodities.Add(receiveDetailAllViewModels);
                    }
                    else
                    {
                        receiveDetailAllViewModels.ReceiveDetailCounter = 1;
                        updateCommodities.Add(receiveDetailAllViewModels);
                    }
                }

                ViewBag.ReceiveDetails = prevCommodities;
                receiveModels.ReceiveDetails = prevCommodities;
                bool isValid = ModelState.IsValid;

                //this check need's to be revisited
                if (prevCommodities.Count() == 0)
                {
                    ModelState.AddModelError("ReceiveDetails", "Please add atleast one commodity to save this Reciept");
                }
                
                //TODO add check against the commodity type for each commodity 
                string errorMessage = null;
                foreach (var receiveDetailViewModel in prevCommodities)
                {
                    var validationContext = new ValidationContext(receiveDetailViewModel, null, null);
                    IEnumerable<ValidationResult> validationResults = receiveDetailViewModel.Validate(validationContext);
                    foreach (var v in validationResults)
                    {
                        errorMessage = string.Format("{0}, {1}", errorMessage, v.ErrorMessage);
                    }
                    Commodity comms = repository.Commodity.FindById(receiveDetailViewModel.CommodityID);
                    CommodityType commType = repository.CommodityType.FindById(receiveModels.CommodityTypeID);
                    if (receiveModels.CommodityTypeID != comms.CommodityTypeID)
                        ModelState.AddModelError("ReceiveDetails", comms.Name + " is not of type " + commType.Name);
                }
                if (errorMessage != null)
                {
                    ModelState.AddModelError("ReceiveDetails", errorMessage);
                }
            }
            else
            {
                ModelState.AddModelError("ReceiveDetails", "Please add atleast one commodity to save this Reciept");
            }
            switch (receiveModels.CommoditySourceID)
            {
                case BLL.CommoditySource.Constants.DONATION:
                    ModelState.Remove("SourceHubID");
                    ModelState.Remove("SupplierName");
                    ModelState.Remove("PurchaseOrder");
                    break;
                case BLL.CommoditySource.Constants.LOCALPURCHASE:
                    //ModelState.Remove("DonorID");
                    ModelState.Remove("SourceHubID");
                    //ModelState.Remove("ResponsibleDonorID");
                    break;
                default:
                    ModelState.Remove("DonorID");
                    ModelState.Remove("ResponsibleDonorID");
                    ModelState.Remove("SupplierName");
                    ModelState.Remove("PurchaseOrder");
                    break;
            }

            if (ModelState.IsValid && user != null)
            {
                if (receiveModels.ChangeStoreManPermanently != null && receiveModels.ChangeStoreManPermanently == true)
                {
                    BLL.Store storeTobeChanged = repository.Store.FindById(receiveModels.StoreID);
                    if (storeTobeChanged != null && receiveModels.ChangeStoreManPermanently == true)
                    storeTobeChanged.StoreManName = receiveModels.ReceivedByStoreMan;
                    //repository.Store.SaveChanges(storeTobeChanged);
                }

                BLL.Receive receive = receiveModels.GenerateReceive();
                //if (receive.ReceiveID == null )
                if(receiveModels.ReceiveID == null)
                {
                    //List<ReceiveDetailViewModel> commodities = GetSelectedCommodities(receiveModels.JSONInsertedCommodities);
                    receiveModels.ReceiveDetails = prevCommodities;
                    foreach (var gridCommodities in prevCommodities)
                    {
                        if (user.PreferedWeightMeasurment.Equals("qn"))
                        {
                            gridCommodities.ReceivedQuantityInMT /= 10;
                            gridCommodities.SentQuantityInMT /= 10;
                        }
                    }
                    repository.Transaction.SaveReceiptTransaction(receiveModels, user);
                }
                else
                {
                    //List<Models.ReceiveDetailViewModel>
                    //insertCommodities = GetSelectedCommodities(receiveModels.JSONInsertedCommodities);
                    List<Models.ReceiveDetailViewModel> deletedCommodities = GetSelectedCommodities(receiveModels.JSONDeletedCommodities);
                   // List<Models.ReceiveDetailViewModel> updateCommodities = GetSelectedCommodities(receiveModels.JSONUpdatedCommodities);
                    receive.HubID = user.DefaultHub.HubID;
                    receive.UserProfileID = user.UserProfileID;
                    receive.Update(GenerateReceiveDetail(insertCommodities),
                        GenerateReceiveDetail(updateCommodities),
                        GenerateReceiveDetail(deletedCommodities));

                }

                return RedirectToAction("Index");
            }
            receiveModels.InitializeEditLists( user );
            if(receiveModels.ReceiveID != null)
            {
                receiveModels.IsEditMode = true;
            }
            return View(receiveModels);
        }



        /// <summary>
        /// Generates the receive detail.
        /// </summary>
        /// <param name="commodities">The commodities passed to the controller from the telerik grid.</param>
        /// <returns></returns>
        private static List<ReceiveDetail> GenerateReceiveDetail(List<Models.ReceiveDetailViewModel> commodities)
        {
            if (commodities != null)
            {
                var comms = commodities.Where(c => c != null).Select(c => new ReceiveDetail()
                                                                              {
                                                                                  CommodityID = c.CommodityID,
                                                                                  //BLL.Commodity.GetCommodityByName(c.CommodityName).CommodityID,
                                                                                  Description = c.Description,
                                                                                  SentQuantityInMT =
                                                                                      c.SentQuantityInMT.Value,
                                                                                  SentQuantityInUnit =
                                                                                      c.SentQuantityInUnit.Value,
                                                                                  UnitID = c.UnitID,
                                                                                  //BLL.Unit.GetUnitByName(c.Unit).UnitID
                                                                                  //ReceiveDetailID = c.ReceiveDetailID.Value                                                                  
                                                                              });

                return comms.ToList();
            }
            else
            {
                return Enumerable.Empty<BLL.ReceiveDetail>().ToList();
            }
        }


        /// <summary>
        /// Jsons the receive.
        /// </summary>
        /// <param name="grnNo">The GRN no.</param>
        /// <returns></returns>
        [GridAction]
        public virtual ActionResult JsonReceive(string grnNo)
        {
            BLL.Receive receive = BLL.Receive.GetReceiveByGRN(grnNo);
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (receive != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {   //send an empty json, and a status number to dispaly that the no receive is found with this GRN
                //return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
                return new EmptyResult();
            }
       }

        /// <summary>
        /// Loads the data by SI.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public ActionResult LoadDataBySI(string SINumber,string receiptAllocationID )
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            string WayBillNo = null;
            string ProjectCode = null;
            string PurchaseOrder = null;
            string SupplierName = null;
            int? SourceHubID = null;
            int? CommodityTypeID = null;
            int? ProgramID = null;
            int? ResponsibleDonorID = null;
            int? SourceDonorID = null;

            if (repository.GiftCertificate.FindBySINumber(SINumber) != null)
            {
                BLL.GiftCertificate gCertificate = repository.GiftCertificate.FindBySINumber(SINumber);
                var giftCertificateDetail = gCertificate.GiftCertificateDetails.FirstOrDefault();
                if (giftCertificateDetail != null)
                {
                    WayBillNo = giftCertificateDetail.BillOfLoading;
                    CommodityTypeID = giftCertificateDetail.Commodity.CommodityTypeID;
                }
                ResponsibleDonorID = gCertificate.DonorID;
                SourceDonorID = gCertificate.DonorID;
                ProgramID = gCertificate.ProgramID;
            }
            
            if (receiptAllocationID != null && receiptAllocationID != "")
            {
                var theAllocation = repository.ReceiptAllocation.FindById(Guid.Parse(receiptAllocationID));
                if(theAllocation!=null)
                {
                   ProjectCode = theAllocation.ProjectNumber;
                   PurchaseOrder = theAllocation.PurchaseOrder;
                   SupplierName = theAllocation.SupplierName;
                   SourceHubID = theAllocation.SourceHubID;
                   CommodityTypeID = theAllocation.Commodity.CommodityTypeID;
                   ProgramID = theAllocation.ProgramID;
                   //ResponsibleDonorID = theAllocation.DonorID;
                   //SourceDonorID = theAllocation.DonorID;

                }
            }
            else{
            if (repository.ReceiptAllocation.FindBySINumber(SINumber) != null &&
                repository.ReceiptAllocation.FindBySINumber(SINumber).Any())
            {
                BLL.ReceiptAllocation rAllocation =
                    repository.ReceiptAllocation.GetAll().FirstOrDefault(
                        p => p.SINumber == SINumber && p.HubID == user.DefaultHub.HubID);

                if (rAllocation != null)
                {
                    ProjectCode = rAllocation.ProjectNumber;
                    PurchaseOrder = rAllocation.PurchaseOrder;
                    SupplierName = rAllocation.SupplierName;
                    SourceHubID = rAllocation.SourceHubID;

                    if (ProgramID == null)
                    {
                        ProgramID = rAllocation.ProgramID;
                    }

                    if (CommodityTypeID == null)
                    {
                        CommodityTypeID = rAllocation.Commodity.CommodityTypeID;
                    }

                }
            }
        }

    return Json( new{ 
                            WayBillNo,
                            ProjectCode,
                            CommodityTypeID,
                            ProgramID,
                            ResponsibleDonorID,
                            SourceDonorID,
                            PurchaseOrder,
                            SupplierName,
                            SourceHubID
                            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Determines whether [SI valid] 
        /// This is to check if the SI number exists in the Shipping Instructions table.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public ActionResult IsSIValid(string SINumber)
        {
            
            bool resultGC = true;
            bool resultRA = true;
            ArrayList result = new ArrayList();
            resultGC = repository.GiftCertificate.FindBySINumber(SINumber) != null;
            resultRA = repository.ReceiptAllocation.FindBySINumber(SINumber) != null;
            result.Add(resultGC);
            result.Add(resultRA);
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        /// <summary>
        /// Gets the allocation balance by SI.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public ActionResult GetSIBalanceBySI(string SINumber)
        {
            var list = repository.GiftCertificate.GetSIBalances();
            return PartialView("SIBalance", list);
        }

        /// <summary>
        /// Determines whether there is a record with the specified GRN Number.
        /// </summary>
        /// <param name="grnNo">The GRN no.</param>
        /// <returns></returns>
        [GridAction]
        public virtual ActionResult isReceiveNull(string grnNo)
        {
            
            BLL.Receive receive = BLL.Receive.GetReceiveByGRN(grnNo);
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            if (receive != null && user.DefaultHub != null)
            {
                if (receive.HubID == user.DefaultHub.HubID)
                {
                    return Json(new { success = true, defaultWareHouse = true }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = true, defaultWareHouse = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        
        }

        /// <summary>
        /// Gets the GRN by receive ID.
        /// </summary>
        /// <param name="receiveId">The receive id.</param>
        /// <returns></returns>
        [GridAction]
        public virtual ActionResult GetGrnByReceiveID(string receiveId)
        {
             //  db.Receives.SingleOrDefault(p => p.ReceiveID == receiveId);

            if (receiveId != null && receiveId != "") // && receiveGrn != null)
            {
                var receiveGrn = repository.Receive.FindById(Guid.Parse(receiveId));
                return Json(new { receive = receiveGrn.GRN }, JsonRequestBehavior.AllowGet);
            }
            else {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// Selects the received commodities for the telerik grid to display
        /// This returns the grid action in an ajax call from the telerik grid.
        /// </summary>
        /// <param name="receiveId">The receive id.</param>
        /// <returns></returns>
        [GridAction]
        public virtual ActionResult SelectReceivedCommodities(string receiveId)
        {
            var commodities = new List<Models.ReceiveDetailViewModel>();
            if (receiveId != "" && receiveId != null)
            {
                commodities = ReceiveDetailViewModel.GenerateReceiveDetailModels(repository.Receive.FindById(Guid.Parse(receiveId)).ReceiveDetails);
                
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

                foreach (var gridCommodities in commodities)
                {
                    if (user.PreferedWeightMeasurment.Equals("qn"))
                    {
                        gridCommodities.ReceivedQuantityInMT *= 10;
                        gridCommodities.SentQuantityInMT *= 10;
                    }
                }
                string str = Request["prev"];
                if (GetSelectedCommodities(str) != null)
                {
                    var allCommodities = GetSelectedCommodities(Request["prev"].ToString());
                    int count = -1;
                    //TODO Revise this section please
                    foreach (var receiveDetailViewModelComms in allCommodities)
                    {
                        if (receiveDetailViewModelComms.ReceiveDetailID == null)
                        {
                            //receiveDetailViewModelComms.ReceiveDetailID = count--;
                            receiveDetailViewModelComms.ReceiveDetailCounter = count--;
                            commodities.Add(receiveDetailViewModelComms);
                        }
                        /**
                         * TODO the lines below are too nice to have but we need to look into the performance issue and 
                         * policies (i.e. editing should not be allowed ) may be only for quanitities 
                          */
                        else //replace the commodity read from the db by what's comming from the user
                        {
                            commodities.Remove(commodities.Find(p => p.ReceiveDetailID == receiveDetailViewModelComms.ReceiveDetailID));
                            commodities.Add(receiveDetailViewModelComms);
                        }
                    }
                }
            }
            else
            {
                string str = Request["prev"];
                if ( GetSelectedCommodities( str ) != null)
                {
                    commodities = GetSelectedCommodities(Request["prev"].ToString()) ;
                    int count = -1;
                    foreach (var rdm in commodities)
                    {
                        //TODO: Revise this section
                        if (rdm.ReceiveDetailID != null)
                        {
                            //rdm.ReceiveDetailID = count--;
                            rdm.ReceiveDetailCounter = count--;
                        }
                    }
                }
            }
          
            ViewData["Commodities"] =  repository.Commodity.GetAllSubCommodities().Select(c => new Models.CommodityModel() { Id = c.CommodityID, Name = c.Name }).ToList();
            ViewData["Units"] = repository.Unit.GetAll().Select(u => new Models.UnitModel() { Id = u.UnitID, Name = u.Name }).ToList();
            return View(new GridModel(commodities));
        }


        /// <summary>
        /// Availbales the commodities.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public ActionResult AvailbaleCommodities(string SINumber, int? commodityTypeId, string receiptAllocationID)
        {

            ArrayList optGroupedList = new ArrayList();
            List <Commodity> comms = new List<Commodity>();
            if (receiptAllocationID != null && receiptAllocationID != "")
            {
                var theComm = repository.ReceiptAllocation.FindById(Guid.Parse(receiptAllocationID)).Commodity;
                comms.Add(theComm);
                if (theComm.ParentID == null)
                    foreach (var childcom in theComm.Commodity1)
                    {
                        comms.Add(childcom);
                    }
            }
            if (!comms.Any())
                comms = repository.ReceiptAllocation.GetAvailableCommodities(SINumber,
                                                                                 repository.UserProfile.GetUser(
                                                                                     User.Identity.Name).DefaultHub.
                                                                                     HubID);
            if (comms.Any())
            {
            foreach (var availableCommodity in comms )
            {
                if(availableCommodity.ParentID == null)
                {
                    optGroupedList.Add(new { Value = availableCommodity.CommodityID, Text = availableCommodity.Name, unselectable = false, id = availableCommodity.ParentID });
                }else
                {
                    optGroupedList.Add(new { Value = availableCommodity.CommodityID, Text = availableCommodity.Name, unselectable = true, id = availableCommodity.ParentID });
                }
            }
            }
            else
            {

                List<Commodity> Parents = new List<Commodity>();
                if (commodityTypeId.HasValue)
                {
                    Parents = repository.Commodity.GetAllParents().Where(p => p.CommodityTypeID == commodityTypeId).ToList();
                }else
                {
                    Parents = repository.Commodity.GetAllParents();
                }

                foreach (Commodity Parent in Parents)
                {
                    var subCommodities = Parent.Commodity1;
                    if (subCommodities != null) //only if it has a subCommodity
                    {
                        optGroupedList.Add(
                            new {Value = Parent.CommodityID, Text = Parent.Name, unselectable = false, id = Parent.ParentID});

                        foreach (Commodity subCommodity in subCommodities)
                        {
                            optGroupedList.Add(
                                new
                                    {
                                        Value = subCommodity.CommodityID,
                                        Text = subCommodity.Name,
                                        unselectable = true,
                                        id = subCommodity.ParentID
                                    });
                        }
                    }
                }
            }
            return Json(optGroupedList,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Allocations the status.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public ActionResult AllocationStatus(string SINumber, int? CommoditySourceID, int? CommodityID, string receiptAllocationID)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            decimal totalAllocation = 0;
            decimal receivedAllocation = 0;
            decimal remainingAllocation = 0;
            string commodity = "";
            int multiplier =  1;
            string mesure = "MT";

            //
           var list = new ArrayList()
                {
                };
            if (SINumber != null)
            {
                // 
                List<Commodity> commodities = new List<Commodity>();

                if (receiptAllocationID != null && receiptAllocationID != "")
                {
                    ReceiptAllocation theCurrentAllocation =
                        repository.ReceiptAllocation.FindById(Guid.Parse(receiptAllocationID));
                    if (theCurrentAllocation != null)
                    {
                        Commodity commodity1 = theCurrentAllocation.Commodity;
                        commodities.Add(commodity1);
                        string commodityName = commodity1.Name;

                        if (commodity1.CommodityTypeID == 1)
                        {

                        decimal tAllocation = theCurrentAllocation.QuantityInMT;
                        
                        decimal recAllocation = repository.ReceiptAllocation.GetReceivedAlready(theCurrentAllocation);

                        list.Add(
                            new
                                {
                                    totalAllocation = tAllocation,
                                    receivedAllocation = recAllocation,
                                    remainingAllocation = tAllocation - recAllocation,
                                    commodity = commodityName,
                                    multiplier = (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN") ? 10 : 1,
                                    mesure = (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN") ? "Qtl" : "MT"
                                });
                        
                        
                        }else
                        {
                            decimal tAllocation = 0;
                            if (theCurrentAllocation.QuantityInUnit != null)
                            {
                                 tAllocation = theCurrentAllocation.QuantityInUnit.Value;
                            }
                            
                            decimal recAllocation = repository.ReceiptAllocation.GetReceivedAlreadyInUnit(theCurrentAllocation);
                               
                                list.Add(
                                    new
                                        {
                                            totalAllocation = tAllocation,
                                            receivedAllocation = recAllocation,
                                            remainingAllocation = tAllocation - recAllocation,
                                            commodity = commodityName,
                                            multiplier = 1,
                                            mesure = "Unit"

                                        });

                                                        
                        }
                }
                    
                }
                else
                {
                    if (!commodities.Any())
                        commodities = repository.ReceiptAllocation.GetAvailableCommoditiesFromUnclosed(SINumber,
                                                                                                       user.DefaultHub.
                                                                                                           HubID, CommoditySourceID);
                    //TODO: make this work for all commodities that are available in the gift certificate

                    if (commodities.Any())
                    {
                        foreach (Commodity commodity1 in commodities)
                        {
                            decimal tAllocation = repository.ReceiptAllocation.GetTotalAllocation(SINumber,
                                                                                                  commodity1.CommodityID,
                                                                                                  user.DefaultHub.HubID,CommoditySourceID);
                            
                            int sI = repository.ShippingInstruction.GetShipingInstructionId(SINumber);

                            //TODO ask the details from elias about the line below
                            decimal sum = 0;
                               if(commodity1.CommodityTypeID == 1){
                                   foreach (ReceiptAllocation rAllocates in commodity1.ReceiptAllocations.Where(p => p.HubID == user.DefaultHub.HubID && p.CommoditySourceID == CommoditySourceID && p.IsClosed == false))
                                    {
                                        sum = sum + repository.ReceiptAllocation.GetReceivedAlready(rAllocates);
                                    }
                               }
                               else
                               {
                                   foreach (ReceiptAllocation rAllocates in commodity1.ReceiptAllocations.Where(p => p.HubID == user.DefaultHub.HubID && p.CommoditySourceID == CommoditySourceID && p.IsClosed == false))
                                   {
                                       sum = sum + repository.ReceiptAllocation.GetReceivedAlreadyInUnit(rAllocates);
                                   }
                               } 
                            decimal recAllocation = sum;

                            string commodityName = commodity1.Name;

                            if (commodity1.CommodityTypeID == 1)
                            {
                                multiplier = (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN") ? 10 : 1;
                                mesure = (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN") ? "Qtl" : "MT";
                            }else
                            {
                                multiplier = 1;
                                mesure = "Unit";
                            }

                            list.Add(
                                new
                                    {
                                        totalAllocation = tAllocation,
                                        receivedAllocation = recAllocation,
                                        remainingAllocation = tAllocation - recAllocation,
                                        commodity = commodityName,
                                        multiplier = multiplier,
                                        mesure = mesure

                                    });
                        }

                    }else //TODO try to get some from receipt allocation
                    {
                        

                    }
                }
            }
            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
} 
