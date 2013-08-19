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
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Controllers
{
    public partial class HubSettingController : BaseController
    {

        IUnitOfWork repository = new UnitOfWork();
        private readonly IHubSettingService _hubSettingService;

        public HubSettingController(IHubSettingService hubSettingService)
        {
            _hubSettingService = hubSettingService;
        }


        public virtual ActionResult Index()
        {
            return View(_hubSettingService.GetAllHubSetting());
            
        }


        public virtual ActionResult ListPartial()
        {
            return PartialView(_hubSettingService.GetAllHubSetting());
        }
        //
        // GET: /WarehouseSetting/Details/5

        public virtual ViewResult Details(int id)
        {
            HubSetting hubSetting = _hubSettingService.FindById(id);
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
                _hubSettingService.AddHubSetting(hubSetting);
                return RedirectToAction("Index");   
            }

            return View(hubSetting);
        }
        
        //
        // GET: /WarehouseSetting/Edit/5

        public virtual ActionResult Edit(int id)
        {
            HubSetting hubsetting = _hubSettingService.FindById(id);
            return PartialView(hubsetting);
        }

        //
        // POST: /WarehouseSetting/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(HubSetting hubSetting)
        {
            if (ModelState.IsValid)
            {
                _hubSettingService.EditHubSetting(hubSetting);
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            return PartialView(hubSetting);
        }

        //
        // GET: /WarehouseSetting/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubSetting hubSetting = _hubSettingService.FindById(id) ;
            return View(hubSetting);
        }

        //
        // POST: /WarehouseSetting/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _hubSettingService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}