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
    public partial class ProgramController : BaseController
    {

        IUnitOfWork repository = new UnitOfWork();

        public ProgramController()
        {
            
        }

        public ProgramController(IUnitOfWork repo)
        {
            repository = repo;
        }

        public virtual ViewResult Index()
        {
            return View(repository.Program.GetAll());
        }


        public virtual ActionResult ListPartial()
        {
            return PartialView(repository.Program.GetAll());
        }
        //
        // GET: /Program/Details/5

        public virtual ViewResult Details(int id)
        {
            Program program = repository.Program.FindById(id);
            return View(program);
        }

        //
        // GET: /Program/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Program/Create

        [HttpPost]
        public virtual ActionResult Create(Program program)
        {
            if (ModelState.IsValid)
            {
                repository.Program.Add(program);
                return Json(new { success = true }); 
            }

            return PartialView(program);
        }

        //
        // GET: /Program/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Program program = repository.Program.FindById(id);
            return PartialView(program);
        }

        //
        // POST: /Program/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Program program)
        {
            if (ModelState.IsValid)
            {
                repository.Program.SaveChanges(program);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(program);
        }

        //
        // GET: /Program/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Program program = repository.Program.FindById(id);
            return View(program);
        }

        //
        // POST: /Program/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repository.Program.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}