using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;

namespace DRMFSS.Web.Controllers
{
    [Authorize]
    public class CommoditySourceController : BaseController
    {
        private IUnitOfWork repository;

        public CommoditySourceController()
        {
            repository = new UnitOfWork();
        }

        //
        // GET: /CommoditySource/

        public ViewResult Index()
        {
            return View(repository.CommoditySource.GetAll().ToList());
        }

        public ActionResult Update()
        {
            return PartialView(repository.CommoditySource.GetAll().ToList());
        }
        //
        // GET: /CommoditySource/Details/5

        public ViewResult Details(int id)
        {
            CommoditySource commoditysource = repository.CommoditySource.FindById(id);
            return View(commoditysource);
        }

        //
        // GET: /CommoditySource/Create

        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /CommoditySource/Create

        [HttpPost]
        public ActionResult Create(CommoditySource commoditysource)
        {
            if (ModelState.IsValid)
            {
                repository.CommoditySource.Add(commoditysource);
                return Json(new { success = true }); 
            }

            return PartialView(commoditysource);
        }
        
        //
        // GET: /CommoditySource/Edit/5

        public ActionResult Edit(int id)
        {
            CommoditySource commoditysource = repository.CommoditySource.FindById(id);
            return PartialView(commoditysource);
        }

        //
        // POST: /CommoditySource/Edit/5

        [HttpPost]
        public ActionResult Edit(CommoditySource commoditysource)
        {
            if (ModelState.IsValid)
            {
                repository.CommoditySource.SaveChanges(commoditysource);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(commoditysource);
        }

        //
        // GET: /CommoditySource/Delete/5

        public ActionResult Delete(int id)
        {
            CommoditySource commoditysource = repository.CommoditySource.FindById(id);
            return View(commoditysource);
        }

        //
        // POST: /CommoditySource/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var delCommoditysource = repository.CommoditySource.FindById(id);
            if (delCommoditysource != null &&
                (delCommoditysource.Receives.Count == 0))
            {

                repository.CommoditySource.DeleteByID(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Source is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return this.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}