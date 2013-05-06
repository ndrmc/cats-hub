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
    public partial class HubOwnerController : BaseController
    {

       

        public HubOwnerController()
        {
            repository = new UnitOfWork();
        }


        public virtual ActionResult Index()
        {
            return View(repository.HubOwner.GetAll());
        }

        public virtual ActionResult Update()
        {
            return PartialView(repository.HubOwner.GetAll());
        }
        
        //
        // GET: /HubOwners/Details/5

        public virtual ViewResult Details(int id)
        {
            HubOwner HubOwner = repository.HubOwner.FindById(id);
            return View(HubOwner);
        }

        //
        // GET: /HubOwners/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /HubOwners/Create

        [HttpPost]
        public virtual ActionResult Create(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                repository.HubOwner.Add(HubOwner);
                return Json(new { success = true }); 
            }

            return PartialView(HubOwner);
        }
        
        //
        // GET: /HubOwners/Edit/5

        public virtual ActionResult Edit(int id)
        {
            HubOwner HubOwner = repository.HubOwner.FindById(id);
            return PartialView(HubOwner);
        }

        //
        // POST: /HubOwners/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                repository.HubOwner.SaveChanges(HubOwner);
                return Json(new { success = true }); 
            }
            return PartialView(HubOwner);
        }

        //
        // GET: /HubOwners/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubOwner HubOwner = repository.HubOwner.FindById(id);
            return View(HubOwner);
        }

        //
        // POST: /HubOwners/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repository.HubOwner.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}