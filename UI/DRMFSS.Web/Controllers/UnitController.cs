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
    public partial class UnitController : BaseController
    {
         private CTSContext db = new CTSContext();

        //
        // GET: /Unit/

        public virtual ViewResult Index()
        {
            return View(db.Units.ToList());
        }

        public virtual ActionResult Update()
        {
            return PartialView(db.Units.ToList());
        }

        //
        // GET: /Unit/Details/5

        public virtual ViewResult Details(int id)
        {
            Unit unit = db.Units.Single(u => u.UnitID == id);
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Unit/Create

        [HttpPost]
        public virtual ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                db.SaveChanges();
                return Json(new { success = true }); 
            }

            return PartialView(unit);
        }
        
        //
        // GET: /Unit/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Unit unit = db.Units.Single(u => u.UnitID == id);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name", unit.UnitID);
            return PartialView(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Attach(unit);
                db.Entry(unit).State=EntityState.Modified;
                db.SaveChanges();                       
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name", unit.UnitID);
            return PartialView(unit);
        }

        //
        // GET: /Unit/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Unit unit = db.Units.Single(u => u.UnitID == id);
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {            
            Unit unit = db.Units.Single(u => u.UnitID == id);
            db.Units.Remove(unit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}