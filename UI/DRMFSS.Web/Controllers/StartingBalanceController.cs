using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.ViewModels;
using Telerik.Web.Mvc;

namespace DRMFSS.Web.Controllers
{

   [Authorize]
    public class StartingBalanceController : BaseController
    {
        //
        // GET: /StartingBalance/
       private IUnitOfWork repository;

       public StartingBalanceController()
        {
            repository = new UnitOfWork();
        }


        public ActionResult Index()
        {
            return View(new List<StartingBalanceViewModel>());
        }

        [GridAction]
        public ActionResult GetListOfStartingBalances()
        {
           BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
           List<StartingBalanceViewModelDto> startBalnceDto = repository.Transaction.GetListOfStartingBalances(user.DefaultHub.HubID);
           return View(new GridModel(startBalnceDto)); 
        }

        //
        // GET: /StartingBalance/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StartingBalance/Create

        public ActionResult Create()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            StartingBalanceViewModel startingBalanceViewModel = new StartingBalanceViewModel(repository, user);
            return PartialView(startingBalanceViewModel);
        } 

        //
        // POST: /StartingBalance/Create

        [HttpPost]
        public ActionResult Create(StartingBalanceViewModel startingBalance)
        {
            //try
            //if(ModelState.IsValid)
            {
                // TODO: Add insert logic here
                BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
                repository.Transaction.SaveStartingBalanceTransaction(startingBalance, user);
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            //catch
            //{
            //    return View();
            //}
        }
        
        //
        // GET: /StartingBalance/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /StartingBalance/Edit/5

        [HttpPost]
        public ActionResult Edit(StartingBalanceViewModel startingBalance)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StartingBalance/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /StartingBalance/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, StartingBalanceViewModel startingBalance)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
