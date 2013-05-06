using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
     [Authorize]
    public partial class TransporterController : BaseController
    {
      

        //
        // GET: /Transporter/

        public virtual ViewResult Index()
        {
            return View(repository.Transporter.GetAll().OrderBy(t => t.Name).ToList());
        }

        public virtual ActionResult Update()
        {
            return PartialView(repository.Transporter.GetAll().OrderBy(t => t.Name).ToList());
        }

        //
        // GET: /Transporter/Details/5

        public virtual ViewResult Details(int id)
        {
            Transporter transporter = repository.Transporter.FindById(id);
            return View(transporter);
        }

        //
        // GET: /Transporter/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Transporter/Create

        [HttpPost]
        public virtual ActionResult Create(Transporter transporter)
        {
            if (!repository.Transporter.IsNameValid(transporter.TransporterID, transporter.Name))
            {
                ModelState.AddModelError("Name", "Transporter Name should be Unique");
            }
            if (ModelState.IsValid)
            {
                repository.Transporter.Add(transporter);
                return Json(new { success = true });   
            }

            return PartialView(transporter);
        }
        
        //
        // GET: /Transporter/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Transporter transporter = repository.Transporter.FindById(id);
            return PartialView(transporter);
        }

        //
        // POST: /Transporter/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Transporter transporter)
        {
            if(!repository.Transporter.IsNameValid(transporter.TransporterID,transporter.Name))
            {
                ModelState.AddModelError("Name","Transporter Name should be Unique");
            }
            if (ModelState.IsValid)
            {
                repository.Transporter.SaveChanges(transporter);
                return Json(new { success = true }); 
            }
            return View(transporter);
        }

        //
        // GET: /Transporter/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Transporter transporter = repository.Transporter.FindById(id);
            return View(transporter);
        }

        //
        // POST: /Transporter/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repository.Transporter.DeleteByID(id);
            return RedirectToAction("Index");
        }

         public JsonResult IsNameValid(int? TransporterID, string Name)
         {
             return Json(repository.Transporter.IsNameValid(TransporterID, Name), JsonRequestBehavior.AllowGet);
         }
    }
}