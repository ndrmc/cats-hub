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
    public partial class HubController : BaseController
    {
       
        IUnitOfWork repository = new UnitOfWork();
     
        public HubController()
        {
            
        }

        // constructor to make testing easy
        public HubController(IUnitOfWork hubRepo)
        {
            repository = hubRepo;
        }

        public virtual ActionResult Index()
        {
            return View(repository.Hub.GetAll().OrderBy(o => o.HubOwner.Name).ThenBy(o => o.Name));
        }

        public virtual ActionResult ListPartial()
        {
            return PartialView(repository.Hub.GetAll().OrderBy(o => o.HubOwner.Name).ThenBy(o => o.Name));
        }

        //
        // GET: /Warehouse/Details/5

        public virtual ViewResult Details(int id)
        {
            return View(repository.Hub.FindById(id));
        }

        //
        // GET: /Warehouse/Create

        public virtual ActionResult Create()
        {
            ViewBag.HubOwnerID = new SelectList(repository.HubOwner.GetAll().OrderBy(o => o.Name), "HubOwnerID", "Name");
            return PartialView();
        } 

        //
        // POST: /Warehouse/Create

        [HttpPost]
        public virtual ActionResult Create(Hub warehouse)
        {
            if (ModelState.IsValid)
            {
                repository.Hub.Add(warehouse);
                return Json(new { success = true }); 
            }

            ViewBag.HubOwnerID = new SelectList(repository.HubOwner.GetAll().OrderBy(o => o.Name), "HubOwnerID", "Name", warehouse.HubOwnerID);
            return PartialView(warehouse);
        }
        
        //
        // GET: /Warehouse/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Hub Hub = repository.Hub.FindById(id);
            ViewBag.HubOwnerID = new SelectList(repository.HubOwner.GetAll().OrderBy(o => o.Name), "HubOwnerID", "Name", Hub.HubOwnerID);
            return PartialView(Hub);
        }

        //
        // POST: /Warehouse/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Hub warehouse)
        {
            if (ModelState.IsValid)
            {
                repository.Hub.SaveChanges(warehouse);
                //return RedirectToAction("Index");
                return Json(new { success = true }); 
            }
            ViewBag.HubOwnerID = new SelectList(repository.HubOwner.GetAll().OrderBy(o => o.Name), "HubOwnerID", "Name", warehouse.HubOwnerID);
            return PartialView(warehouse);
        }

        //
        // GET: /Warehouse/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View(repository.Hub.FindById(id));
        }

        //
        // POST: /Warehouse/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repository.Hub.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}