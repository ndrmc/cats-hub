using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels;
using Telerik.Web.Mvc;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.Web.Controllers
{
    [Authorize]
    public class LossesAndAdjustmentsController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(repository.Adjustment.GetAllLossAndAdjustmentLog(UserProfile.DefaultHub.HubID).OrderByDescending(c => c.Date));
        }

        public ActionResult CreateLoss()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            LossesAndAdjustmentsViewModel viewModel = new LossesAndAdjustmentsViewModel(repository, user, 1);

            return View(viewModel);
        }

        public ActionResult CreateAdjustment()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            LossesAndAdjustmentsViewModel viewModel = new LossesAndAdjustmentsViewModel(repository, user, 2);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateLoss(LossesAndAdjustmentsViewModel viewModel)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            LossesAndAdjustmentsViewModel newViewModel = new LossesAndAdjustmentsViewModel(repository, user, 1);
            
                           
            if (viewModel.QuantityInMt > repository.Transaction.GetCommodityBalanceForStore(viewModel.StoreId, viewModel.CommodityId, viewModel.ShippingInstructionId, viewModel.ProjectCodeId))
            {
                ModelState.AddModelError("QuantityInMT", "You have nothing to loss");
                return View(newViewModel);
            }

            if (viewModel.QuantityInMt <= 0)
            {
                ModelState.AddModelError("QuantityInMT", "You have nothing to loss");

                return View(newViewModel);
            }
            viewModel.IsLoss = true;
            repository.Adjustment.AddNewLossAndAdjustment(viewModel, user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateAdjustment(LossesAndAdjustmentsViewModel viewModel)
        {
            LossesAndAdjustmentsViewModel newViewModel = new LossesAndAdjustmentsViewModel();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            viewModel.IsLoss = false;
            repository.Adjustment.AddNewLossAndAdjustment(viewModel, user);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult GetStoreMan(int? storeId)
        {
            string storeMan = String.Empty;
            if (storeId != null)
            {
                storeMan = repository.Store.FindById(storeId.Value).StoreManName;
            }
            return Json(storeMan, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Log()
        {

            return View(repository.Adjustment.GetAllLossAndAdjustmentLog(UserProfile.DefaultHub.HubID));
        }

        public ActionResult Filter()
        {
            return PartialView();
        }



        public ActionResult GetStacksForToStore(int? ToStoreId)
        {
            return Json(new SelectList(repository.Store.GetStacksByStoreId(ToStoreId), JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetProjecCodetForCommodity(int? CommodityId)
        {
            var projectCodes = repository.ProjectCode.GetProjectCodesForCommodity(UserProfile.DefaultHub.HubID, CommodityId.Value);
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

        public ActionResult ViewDetial(string TransactionId)
        {
            var lossAndAdjustment = repository.Adjustment.GetAllLossAndAdjustmentLog(UserProfile.DefaultHub.HubID).FirstOrDefault(c => c.TransactionId == Guid.Parse(TransactionId));
            return PartialView(lossAndAdjustment);
        }
        [HttpPost]
        public ActionResult GetFilters()
        {
            var filters = new List<SelectListItem>();
            filters.Add(new SelectListItem { Value = "L", Text = "Loss"});
            filters.Add(new SelectListItem { Value ="A", Text ="Adjustment"});
            return Json(new SelectList(filters, "Value", "Text"));
        }
        [GridAction]
        public ActionResult FilteredGrid(string filterId)
        {
            
            if (filterId != null && filterId != string.Empty)
            {
                var lossAndAdjustmentLogViewModel = repository.Adjustment.GetAllLossAndAdjustmentLog(UserProfile.DefaultHub.HubID).Where(c => c.Type == filterId).OrderByDescending(c => c.Date);
                return View(new GridModel(lossAndAdjustmentLogViewModel));
            }
            return View(new GridModel(repository.Adjustment.GetAllLossAndAdjustmentLog(UserProfile.DefaultHub.HubID).OrderByDescending(c => c.Date)));
        }

        
        public ActionResult GetStoreForParentCommodity(int? commodityParentId, int? SINumber)
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


        public ActionResult SINumberBalance(int? parentCommodityId, int? projectcode, int? SINumber, int? StoreId, int? StackId)
        {
            StoreBalanceViewModel viewModel = new StoreBalanceViewModel();
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (!StoreId.HasValue && !StackId.HasValue && parentCommodityId.HasValue && projectcode.HasValue && SINumber.HasValue)
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
