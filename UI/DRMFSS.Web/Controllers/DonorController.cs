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
    public class DonorController : BaseController
    {
        
        public  ViewResult Index()
        {
            return View(repository.Donor.GetAll().OrderBy(o=>o.Name).ToList());
        }


        public  ActionResult ListPartial()
        {
            return PartialView(repository.Donor.GetAll().OrderBy(o => o.Name).ToList());
        }

        //
        // GET: /Donor/Details/5

        public  ViewResult Details(int id)
        {
            Donor donor = repository.Donor.FindById(id);
            return View(donor);
        }

        //
        // GET: /Donor/Create

        public  ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Donor/Create

        [HttpPost]
        public  ActionResult Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                repository.Donor.Add(donor);
                return Json(new { success = true });  
            }

            return PartialView(donor);
        }
        
        //
        // GET: /Donor/Edit/5

        public  ActionResult Edit(int id)
        {
            Donor donor = repository.Donor.FindById(id);
            return PartialView(donor);
        }

        //
        // POST: /Donor/Edit/5

        [HttpPost]
        public  ActionResult Edit(Donor donor)
        {
            if (ModelState.IsValid && repository.Donor.IsCodeValid(donor.DonorCode,donor.DonorID))
            {
                repository.Donor.SaveChanges(donor);
                return Json(new { success = true });  
            }
            return PartialView(donor);
        }

        //
        // GET: /Donor/Delete/5

        public  ActionResult Delete(int id)
        {
            Donor donor = repository.Donor.FindById(id);
            return View(donor);
        }

        //
        // POST: /Donor/Delete/5

        [HttpPost, ActionName("Delete")]
        public  ActionResult DeleteConfirmed(int id)
        {
            repository.Donor.DeleteByID(id);
            return RedirectToAction("Index");
        }

        public JsonResult IsCodeValid(string DonorCode, int? DonorID)
        {
            return Json(repository.Donor.IsCodeValid(DonorCode, DonorID), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}