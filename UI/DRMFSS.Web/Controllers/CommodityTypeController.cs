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
    public class CommodityTypeController : BaseController
    {
        private IUnitOfWork repository;
        //
        // GET: /CommodityType/

        public CommodityTypeController()
        {
            repository = new UnitOfWork();
        }


        public CommodityTypeController(IUnitOfWork _repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View("Index", repository.CommodityType.GetAll().ToList());
        }

        public ActionResult Update()
        {
            return PartialView("Update",repository.CommodityType.GetAll().ToList());
        }
        //
        // GET: /CommodityType/Details/5

        public ViewResult Details(int id)
        {
            CommodityType commoditytype =repository.CommodityType.FindById(id);
            return View(commoditytype);
        }

        //
        // GET: /CommodityType/Create

        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /CommodityType/Create

        [HttpPost]
        public ActionResult Create(CommodityType commoditytype)
        {
            if (ModelState.IsValid)
            {
                repository.CommodityType.Add(commoditytype);
                return Json(new { success = true });  
            }

            return PartialView(commoditytype);
        }
        
        //
        // GET: /CommodityType/Edit/5

        public ActionResult Edit(int id)
        {
            CommodityType commoditytype = repository.CommodityType.FindById(id);
            return PartialView(commoditytype);
        }

        //
        // POST: /CommodityType/Edit/5

        [HttpPost]
        public ActionResult Edit(CommodityType commoditytype)
        {

            if (ModelState.IsValid)
            {
                repository.CommodityType.SaveChanges(commoditytype);
                return Json(new { success = true });
            }
           // ViewBag.CommodityTypeID = new SelectList(db.Warehouses, "CommodityTypeID", "Name", store.WarehouseID);
            return PartialView(commoditytype);
        }

        //
        // GET: /CommodityType/Delete/5

        public ActionResult Delete(int id)
        {
            
            CommodityType commoditytype = repository.CommodityType.FindById(id);
            return View(commoditytype);
        }

        //
        // POST: /CommodityType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Commodity commodity = CommodityRepo.FindById(id);
            var delCommodityType = repository.CommodityType.FindById(id);
            if (delCommodityType != null &&
                (delCommodityType.Commodities.Count == 0))
            {

                repository.CommodityType.DeleteByID(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Type is referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return this.Delete(id);
        }

        [HttpPost]
        public ActionResult _GetCommodityTypes()
        {
                List<CommodityType> result = new List<CommodityType>();
                result = repository.CommodityType.GetAll();
                return new JsonResult
                {
                    Data = new SelectList(result.ToList(), "CommodityTypeID", "Name")
                };

        }


        protected override void Dispose(bool disposing)
        {
           // db.Dispose();
            base.Dispose(disposing);
        }
    }
}