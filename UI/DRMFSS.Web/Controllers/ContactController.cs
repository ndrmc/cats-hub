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
    public class ContactController : BaseController
    {
        private DRMFSSEntities1 db = new DRMFSSEntities1();

        private IUnitOfWork repository;

        public ContactController(IUnitOfWork repository)
        {
            this.repository = repository;
        }
        public ContactController()
        {
            repository = new UnitOfWork();
        }
        //
        // GET: /Contact/

        public ViewResult Index(int? fdpId)
        {
            if (fdpId.HasValue)
            {
                var contacts = repository.Contact.GetByFdp(fdpId.Value);
                ViewBag.FDPID = fdpId.Value;
                ViewBag.FDPName = repository.FDP.FindById(fdpId.Value).Name;
                return View("Index", contacts.ToList());
            }
            else
            {
                var contacts = repository.Contact.GetAll();
                return View("Index", contacts);
            }
        }

        //
        // GET: /Contact/Details/5

        public ViewResult Details(int id)
        {
            Contact contact = repository.Contact.FindById(id);
            ViewBag.FDPName = contact.FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Details", contact);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create(int fdpId)
        {
            ViewBag.FDPName = repository.Contact.FindById(fdpId).FDP.Name;
            ViewBag.FDPID = fdpId;
            Contact contact = new Contact() { FDPID = fdpId };
            return View("Create", contact);
        } 

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                repository.Contact.Add(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });  
            }
            ViewBag.FDPName = repository.Contact.FindById(contact.ContactID).FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Create", contact);
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            Contact contact = repository.Contact.FindById(id);
            ViewBag.FDPName = contact.FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Edit", contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                repository.Contact.SaveChanges(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });
            }

            ViewBag.FDPName = repository.Contact.FindById(contact.FDPID).FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Edit", contact);
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            Contact contact = repository.Contact.FindById(id);
            ViewBag.FDPID = contact.FDPID;
            return View("Delete", contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = repository.Contact.FindById(id);
            try
            {
                repository.Contact.Delete(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });
            }
            catch (Exception e)
            {
                return View("Delete", contact);
            }
        }

        protected override void Dispose(bool disposing)
        {
          
            base.Dispose(disposing);
        }
    }
}
