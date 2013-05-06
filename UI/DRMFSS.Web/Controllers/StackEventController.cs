using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL;
using Telerik.Web.Mvc;

namespace DRMFSS.Web.Controllers
{
    public class StackEventController : BaseController
    {
        //
        // GET: /StackEvent/
        IUnitOfWork repository;

        public StackEventController()
        {
            repository = new UnitOfWork();
        }

        public ActionResult Index()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            StackEventViewModel viewModel = new StackEventViewModel(repository, user);
            return View(viewModel );
        }
        
        public ActionResult EventLog()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            List<StackEventLogViewModel> viewModel = new List<StackEventLogViewModel>();
                //repository.StackEvent.GetAllStackEvents(user);
            return PartialView(viewModel);
        }

        [GridAction]
        public ActionResult EventLogGrid(int? StackId, int? StoreId)
        {
            if (StackId.HasValue && StoreId.HasValue)
            {
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                return View(new GridModel(repository.StackEvent.GetAllStackEventsByStoreIdStackId(user,StackId.Value, StoreId.Value).OrderByDescending(o => o.EventDate)));
            }
            return View(new GridModel(new List<StackEventViewModel>()));
        }

        public ActionResult EditStackEvent()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            StackEventViewModel viewModel = new StackEventViewModel(repository, user);
            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult EditStackEvent(StackEventViewModel viewModel)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            repository.StackEvent.Add(new StackEvent 
            { 
                EventDate = viewModel.EventDate,
                StoreID = viewModel.StoreIdTwo,
                StackEventTypeID = viewModel.StackEventTypeId,
                StackNumber = viewModel.StackNumberTwo,
                FollowUpDate = viewModel.FollowUpDate,
                FollowUpPerformed = false,
                Description = viewModel.Description,
                Recommendation  = viewModel.Recommendation,
                UserProfileID = user.UserProfileID
            });
            return RedirectToAction("Index", "StackEvent");
        }

        public ActionResult GetStacksFromStore(int? StoreId)
        {
            return Json(new SelectList(repository.Store.GetStacksByStoreId(StoreId), JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetStacksFromStoreTwo(int? StoreIdTwo)
        {
            return Json(new SelectList(repository.Store.GetStacksByStoreId(StoreIdTwo), JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public ActionResult GetStore()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            return new JsonResult { Data = new SelectList(repository.Hub.GetAllStoreByUser(user), "StoreId", "StoreName") };
        }

        [HttpPost]
        public ActionResult GetEventType()
        {
            return new JsonResult { Data = new SelectList(repository.StackEventType.GetAll(), "StackEventTypeID", "Name") };
        }

        [HttpPost]
        public ActionResult GetFollowUpDate(DateTime selectedDate, int StackEventTypeId)
        {
            DateTime followupDate = DateTime.Now;
            if (selectedDate != null)
            {
                followupDate = selectedDate;
                var duration = repository.StackEventType.GetFollowUpDurationByStackEventTypeId(StackEventTypeId);

                followupDate = followupDate.AddDays(duration);
            }
            return Json(followupDate.ToShortDateString(), JsonRequestBehavior.AllowGet);
        }
    }
}
