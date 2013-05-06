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
    public partial class HubSettingController : BaseController
    {

        IUnitOfWork repository = new UnitOfWork();

        public virtual ActionResult Index()
        {
            return View(repository.HubSetting.GetAll());
        }


        public virtual ActionResult ListPartial()
        {
            return PartialView(repository.HubSetting.GetAll());
        }
        //
        // GET: /WarehouseSetting/Details/5

        public virtual ViewResult Details(int id)
        {
            HubSetting hubSetting = repository.HubSetting.FindById(id);
            return View(hubSetting);
        }

        //
        // GET: /WarehouseSetting/Create

        public virtual ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /WarehouseSetting/Create

        [HttpPost]
        public virtual ActionResult Create(HubSetting hubSetting)
        {
            if (ModelState.IsValid)
            {
                repository.HubSetting.Add(hubSetting);
                return RedirectToAction("Index");   
            }

            return View(hubSetting);
        }
        
        //
        // GET: /WarehouseSetting/Edit/5

        public virtual ActionResult Edit(int id)
        {
            HubSetting hubsetting = repository.HubSetting.FindById(id);
            return PartialView(hubsetting);
        }

        //
        // POST: /WarehouseSetting/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(HubSetting hubSetting)
        {
            if (ModelState.IsValid)
            {
                repository.HubSetting.SaveChanges(hubSetting);
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            return PartialView(hubSetting);
        }

        //
        // GET: /WarehouseSetting/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubSetting hubSetting = repository.HubSetting.FindById(id) ;
            return View(hubSetting);
        }

        //
        // POST: /WarehouseSetting/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repository.HubSetting.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}