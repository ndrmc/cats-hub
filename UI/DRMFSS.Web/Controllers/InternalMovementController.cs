using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.Web.Controllers
{
    public class InternalMovementController : BaseController
    {
        //
        // GET: /InternalMovement/
        private IUnitOfWork repository;

        public InternalMovementController()
        {
            repository = new UnitOfWork();
        }

        public ActionResult Index()
        {
            return View(repository.InternalMovement.GetAllInternalMovmentLog().OrderByDescending(c => c.SelectedDate));
        }

        public ActionResult Create()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            InternalMovementViewModel viewModel = new InternalMovementViewModel(repository, user);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(InternalMovementViewModel viewModel)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            InternalMovementViewModel newViewModel = new InternalMovementViewModel(repository, user);
            if (viewModel.QuantityInMt > repository.Transaction.GetCommodityBalanceForStack(viewModel.FromStoreId, viewModel.FromStackId, viewModel.CommodityId, viewModel.ShippingInstructionId, viewModel.ProjectCodeId))
            {
                ModelState.AddModelError("QuantityInMt", "you dont have sufficent ammout to transfer");
                return View(newViewModel);
            }
            if (viewModel.QuantityInMt <= 0)
            {
                ModelState.AddModelError("QuantityInMt", "You have nothing to transfer");
                return View(newViewModel);
            }

            repository.InternalMovement.AddNewInternalMovement(viewModel, user);
            return RedirectToAction("Index", "InternalMovement");
        }

        public ActionResult IsQuantityValid(decimal QuantityInMt, int? FromStoreId, int? FromStackId, int? CommodityId, int? ShippingInstructionId, int? ProjectCodeId  )
        {
            bool result = true;
            if (FromStoreId.HasValue && CommodityId.HasValue && ShippingInstructionId.HasValue && ProjectCodeId.HasValue)
            {

                if ((QuantityInMt > repository.Transaction.GetCommodityBalanceForStack(FromStoreId.Value, FromStackId.Value, CommodityId.Value, ShippingInstructionId.Value, ProjectCodeId.Value)))
                {
                    result = false;
                }

            }
            else
            {
                result = false;
            }
            return (Json(result, JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetStacksForFromStore(int? FromStoreId, int? SINumber)
        {
            if (FromStoreId.HasValue && SINumber.HasValue)
            {
                return Json(new SelectList(repository.Store.GetStacksWithSIBalance(FromStoreId.Value, SINumber.Value), JsonRequestBehavior.AllowGet));
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }
        }

        public ActionResult GetStacksForToStore(int? ToStoreId, int? FromStoreId, int? FromStackId)
        {
            if (ToStoreId.HasValue && FromStackId.HasValue && FromStoreId.HasValue)
            {
                return Json(new SelectList(repository.Store.GetStacksByToStoreIdFromStoreIdFromStack(ToStoreId.Value, FromStoreId.Value, FromStackId.Value), JsonRequestBehavior.AllowGet));
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }
        }

        public ActionResult GetProjecCodetForCommodity(int? CommodityId)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            var projectCodes = repository.ProjectCode.GetProjectCodesForCommodity(user.DefaultHub.HubID, CommodityId.Value);
            return Json(new SelectList(projectCodes, "ProjectCodeId", "ProjectName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSINumberForProjectCode(int? ProjectCodeId)
        {
            if (ProjectCodeId.HasValue)
            {
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                return Json(new SelectList(repository.ShippingInstruction.GetShippingInstructionsForProjectCode(user.DefaultHub.HubID, ProjectCodeId.Value), "ShippingInstructionId", "ShippingInstructionName"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<ShippingInstructionViewModel>()));
            }

        }

        public ActionResult GetFromStoreForParentCommodity(int? commodityParentId, int? SINumber)
        {
            if (commodityParentId.HasValue && SINumber.HasValue)
            {
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                return Json(new SelectList(ConvertStoreToStoreViewModel(repository.Store.GetStoresWithBalanceOfCommodityAndSINumber(commodityParentId.Value, SINumber.Value, user.DefaultHub.HubID)), "StoreId", "StoreName"));
            }
            else
            {
                return Json(new SelectList(new List<StoreViewModel>()));
            }
        }

        public ActionResult ViewDetial(string TransactionId)
        {
            var internalMovment = repository.InternalMovement.GetAllInternalMovmentLog().FirstOrDefault(c => c.TransactionId == Guid.Parse(TransactionId));
            return PartialView(internalMovment);
        }

        public ActionResult SINumberBalance(int? parentCommodityId,int? projectcode, int? SINumber, int? StoreId, int? StackId)
        {
            StoreBalanceViewModel viewModel = new StoreBalanceViewModel();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if(!StoreId.HasValue && !StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = repository.Commodity.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = repository.ProjectCode.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = repository.ShippingInstruction.FindById(SINumber.Value).Value;
                viewModel.QtBalance = repository.Transaction.GetCommodityBalanceForHub(user.DefaultHub.HubID, parentCommodityId.Value, SINumber.Value, projectcode.Value);
            }
            else if (StoreId.HasValue && !StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = repository.Commodity.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = repository.ProjectCode.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = repository.ShippingInstruction.FindById(SINumber.Value).Value;
                viewModel.QtBalance = repository.Transaction.GetCommodityBalanceForStore(StoreId.Value, parentCommodityId.Value, SINumber.Value, projectcode.Value);
                var store = repository.Store.FindById(StoreId.Value);
                viewModel.StoreNameB = string.Format("{0} - {1}", store.Name, store.StoreManName);
            }

            else if (StoreId.HasValue && StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
            {
                viewModel.ParentCommodityNameB = repository.Commodity.FindById(parentCommodityId.Value).Name;
                viewModel.ProjectCodeNameB = repository.ProjectCode.FindById(projectcode.Value).Value;
                viewModel.ShppingInstructionNumberB = repository.ShippingInstruction.FindById(SINumber.Value).Value;
                viewModel.QtBalance = repository.Transaction.GetCommodityBalanceForStack(StoreId.Value, StackId.Value, parentCommodityId.Value, SINumber.Value, projectcode.Value);
                var store = repository.Store.FindById(StoreId.Value);
                viewModel.StoreNameB = string.Format("{0} - {1}", store.Name, store.StoreManName);
                viewModel.StackNumberB = StackId.Value.ToString();
            }
                
            return PartialView(viewModel);
        }


        List<StoreViewModel> ConvertStoreToStoreViewModel(List<Store> Stores)
        {
            List<StoreViewModel> viewModel = new List<StoreViewModel>();
            foreach (var store in Stores)
            {
                viewModel.Add(new StoreViewModel { StoreId = store.StoreID, StoreName = string.Format("{0} - {1} ", store.Name, store.StoreManName) });
            }

            return viewModel;
        }
        
    }
}
