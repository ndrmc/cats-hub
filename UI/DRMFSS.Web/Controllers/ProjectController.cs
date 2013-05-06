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
    public class ProjectController : BaseController
    {
        private DRMFSSEntities1 db = new DRMFSSEntities1();

        //
        // GET: /Project/

        public ViewResult Index()
        {
            return View(db.Projects.ToList());
        }

        public ActionResult Update()
        {
            return PartialView(db.Projects.ToList());
        }
        //
        // GET: /Project/Details/5

        public ViewResult Details(int id)
        {
            Project project = db.Projects.Single(p => p.ProjectID == id);
            return View(project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.AddObject(project);
                db.SaveChanges();
                return Json(new { success = true }); 
            }

            return PartialView(project);
        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(int id)
        {
            Project project = db.Projects.Single(p => p.ProjectID == id);
            return PartialView(project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Attach(project);
                db.ObjectStateManager.ChangeObjectState(project, EntityState.Modified);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            return View(project);
        }

        //
        // GET: /Project/Delete/5

        public ActionResult Delete(int id)
        {
            Project project = db.Projects.Single(p => p.ProjectID == id);
            return View(project);
        }

        //
        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Single(p => p.ProjectID == id);
            db.Projects.DeleteObject(project);
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