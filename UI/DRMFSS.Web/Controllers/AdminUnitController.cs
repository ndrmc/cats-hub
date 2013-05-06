using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL.Repository;
using Telerik.Web.Mvc;
using DRMFSS.Web.Models;
using DRMFSS.BLL;
using Telerik.Web.Mvc.UI;

namespace DRMFSS.Web.Controllers
{

    public class AdminUnitController : BaseController
    {


        public AdminUnitController(IUnitOfWork repository)
        {
            this.repository = repository;
        }


        public AdminUnitController()
        {

        }

        [Authorize]
        public ActionResult Index()
        {
            var types = repository.AdminUnit.GetAdminUnitTypes();
            return View("Index", types);
        }

        [Authorize]
        public ActionResult AdminUnits(int? id)
        {
            if (id == null)
            {
                return new EmptyResult();
            }
            else
            {
                ViewBag.Regions = repository.AdminUnit.GetRegions();
                var type = repository.AdminUnit.GetAdminUnitType(id.Value);
                ViewBag.Title = type.Name + "s";
                ViewBag.SelectedTypeId = id;
                var list = type.AdminUnits.OrderBy(a => a.Name);
                //.Select(s => new Models.AdminUnitItem()
                //{ Id = s.AdminUnitID, Name = s.Name});
                if (id == 3)
                {
                    list = type.AdminUnits.OrderBy(a => a.AdminUnit2.Name).
                        ThenBy(a => a.Name);
                    //.Select(s => 
                    //    new Models.AdminUnitItem() { Id = s.AdminUnitID, Name = s.Name });
                }
                else if (id == 4)
                {
                    list = type.AdminUnits.OrderBy(a => a.AdminUnit2.AdminUnit2.Name).
                        ThenBy(a => a.AdminUnit2.Name).
                        ThenBy(a => a.Name);
                    //.Select(s => 
                    //    new Models.AdminUnitItem() { Id = s.AdminUnitID, Name = s.Name });
                }
                var viewName = "Lists/AdminUnits." + id + "";
                return PartialView(viewName, list);
            }

        }

        //
        // GET: /AdminUnit/Create
        [Authorize]
        public ActionResult Create(int typeid)
        {
            Models.AdminUnitModel model = new Models.AdminUnitModel();
            switch (typeid)
            {
                case 2:
                    model.SelectedAdminUnitTypeId = Shared.Configuration.RegionTypeId;
                    return PartialView("CreateRegion", model);
                case 3:
                    model.SelectedAdminUnitTypeId = Shared.Configuration.ZoneTypeId;
                    return PartialView("CreateZone", model);
                case 4:
                    model.SelectedAdminUnitTypeId = Shared.Configuration.WoredaTypeId;
                    return View("CreateWoreda", model);
                default:
                    model.SelectedAdminUnitTypeId = Shared.Configuration.RegionTypeId;
                    return PartialView("CreateRegion", model);
            }

        }

        [Authorize]
        public ActionResult CreateRegion()
        {
            Models.AdminUnitModel model = new Models.AdminUnitModel();
            model.SelectedAdminUnitTypeId = Shared.Configuration.RegionTypeId;
            return PartialView("CreateRegion", model);
        }

        [Authorize]
        public ActionResult CreateZone(int? regionId)
        {
            Models.AdminUnitModel model = new Models.AdminUnitModel();
            if (regionId.HasValue)
            {
                BLL.AdminUnit region = repository.AdminUnit.FindById(regionId.Value);
                model.SelectedRegionId = region.AdminUnitID;
                model.SelectedRegionName = region.Name;
            }
            model.SelectedAdminUnitTypeId = Shared.Configuration.ZoneTypeId;
            return PartialView("CreateZone", model);
        }

        public ActionResult CreateWoreda(int? zoneId)
        {
            Models.AdminUnitModel model = new Models.AdminUnitModel();

            model.SelectedAdminUnitTypeId = Shared.Configuration.WoredaTypeId;
            if (zoneId.HasValue)
            {
                BLL.AdminUnit zone = repository.AdminUnit.FindById(zoneId.Value);
                model.SelectedZoneName = zone.Name;
                model.SelectedZoneId = zone.AdminUnitID;
                model.SelectedRegionId = zone.AdminUnit2.AdminUnitID;
                model.SelectedRegionName = zone.AdminUnit2.Name;
            }
            return PartialView("CreateWoreda", model);
        }

        //
        // POST: /AdminUnit/Create

        [HttpPost]
        public ActionResult Create(Models.AdminUnitModel unit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BLL.AdminUnit aunit = new BLL.AdminUnit();
                    aunit.AdminUnitTypeID = unit.SelectedAdminUnitTypeId;
                    if (aunit.AdminUnitTypeID == Shared.Configuration.ZoneTypeId)
                    {
                        aunit.ParentID = unit.SelectedRegionId;
                    }
                    else if (aunit.AdminUnitTypeID == Shared.Configuration.WoredaTypeId)
                    {
                        aunit.ParentID = unit.SelectedZoneId;
                    }
                    aunit.Name = unit.UnitName;
                    aunit.NameAM = unit.UnitNameAM;

                    repository.AdminUnit.Add(aunit);


                    return Json(new {success = true});
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return View("Create");
        }

        //
        // GET: /AdminUnit/Edit/5

        public ActionResult Edit(int id)
        {
            BLL.AdminUnit unit = repository.AdminUnit.FindById(id);
            return PartialView("Edit", unit);
        }

        //
        // POST: /AdminUnit/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BLL.AdminUnit unit)
        {

            if (ModelState.IsValid)
            {
                repository.AdminUnit.SaveChanges(unit);
                return Json(new {success = true});
            }
            return PartialView("Edit", unit);
        }

        //
        // GET: /AdminUnit/Delete/5

        public ActionResult Delete(int id)
        {
            BLL.AdminUnit unit = repository.AdminUnit.FindById(id);
            return View("Delete", unit);
        }

        //
        // POST: /AdminUnit/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, BLL.AdminUnit unit)
        {
            try
            {
                // TODO: Add delete logic here
                repository.AdminUnit.Delete(unit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }

        public ActionResult GetRegions()
        {
            var units = from item in repository.AdminUnit.GetRegions()
                        select new Models.AdminUnitItem
                                   {
                                       Id = item.AdminUnitID,
                                       Name = item.Name
                                   };
            return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildren(int? unitId)
        {
            if (unitId.HasValue)
            {
                var units = from item in repository.AdminUnit.GetChildren(unitId.Value)
                            select new Models.AdminUnitItem
                                       {
                                           Id = item.AdminUnitID,
                                           Name = item.Name
                                       };
                return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<Models.AdminUnitItem>(), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetChildrenReport(int? unitId)
        {
            if (unitId.HasValue)
            {
                var units = (from item in repository.AdminUnit.GetChildren(unitId.Value)
                             select new Models.AdminUnitItem
                                        {
                                            Id = item.AdminUnitID,
                                            Name = item.Name
                                        }).ToList();
                return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<Models.AdminUnitItem>(), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetZones(int? SelectedRegionId)
        {
            if (SelectedRegionId == null)
            {
                //
                if (Request["SelectedRegionId2"] != null)
                {
                    SelectedRegionId = Convert.ToInt32(Request["SelectedRegionId2"]);
                }
                else if (Request["RegionID"] != null)
                {
                    SelectedRegionId = Convert.ToInt32(Request["RegionID"]);
                }
            }
            return GetChildren(SelectedRegionId);
        }


        public ActionResult GetWoredas(int? SelectedZoneId)
        {
            if (SelectedZoneId == null)
            {
                if (Request["SelectedZoneId2"] != null)
                {
                    SelectedZoneId = Convert.ToInt32(Request["SelectedZoneId2"]);
                }
                else if (Request["ZoneID"] != null)
                {
                    SelectedZoneId = Convert.ToInt32(Request["ZoneID"]);
                }
            }
            return GetChildren(SelectedZoneId);
        }

        [GridAction]
        public ActionResult GetAdminUnitsByParent(int? parentId)
        {
            if (parentId.HasValue)
            {
                var units = from item in repository.AdminUnit.GetChildren(parentId.Value)
                            orderby item.Name
                            select new
                                       {
                                           AdminUnitID = item.AdminUnitID,
                                           Name = item.Name,
                                           AdminUnit2 = new
                                                            {
                                                                Name = item.AdminUnit2.Name,
                                                                AdminUnit2 = new
                                                                                 {
                                                                                     Name =
                                item.AdminUnit2.AdminUnit2.Name
                                                                                 }
                                                            },
                                       };
                return View(new GridModel(units));
            }
            var woredas = from item in repository.AdminUnit.GetAllWoredas()
                          orderby item.Name
                          select new
                                     {
                                         AdminUnitID = item.AdminUnitID,
                                         Name = item.Name,
                                         AdminUnit2 = new
                                                          {
                                                              Name = item.AdminUnit2.Name,
                                                              AdminUnit2 = new
                                                                               {
                                                                                   Name =
                              item.AdminUnit2.AdminUnit2.Name
                                                                               }
                                                          },
                                     };
            return View(new GridModel(woredas));

        }

        [GridAction]
        public ActionResult GetWoredasByParent(int? regionId, int? zoneId)
        {
            List<BLL.AdminUnit> units = null;
            if (zoneId.HasValue)
            {
                units = repository.AdminUnit.GetChildren(zoneId.Value);

            }
            else if (regionId.HasValue)
            {
                units = repository.AdminUnit.GetWoredasByRegion(regionId.Value);
            }


            if (units != null)
            {

                var woredas = from item in units
                              select new
                                         {
                                             AdminUnitID = item.AdminUnitID,
                                             Name = item.Name,
                                             AdminUnit2 = new
                                                              {
                                                                  Name = item.AdminUnit2.Name,
                                                                  AdminUnit2 = new
                                                                                   {
                                                                                       Name =
                                  item.AdminUnit2.AdminUnit2.Name
                                                                                   }
                                                              },
                                         };
                return View(new GridModel(woredas));
            }
            return View(new GridModel(new List<object>()));

        }

        [GridAction]
        public ActionResult GetZonesByParent(int? regionId)
        {
            List<BLL.AdminUnit> units = new List<BLL.AdminUnit>();

            if (regionId.HasValue)
            {
                units = repository.AdminUnit.GetChildren(regionId.Value);
            }


            var woredas = from item in units
                          select new
                                     {
                                         AdminUnitID = item.AdminUnitID,
                                         Name = item.Name,
                                         AdminUnit2 = new
                                                          {
                                                              Name = item.AdminUnit2.Name,
                                                          },
                                     };

            return View(new GridModel(woredas));
        }

        public ActionResult GetTreeElts(TreeViewItem node, bool? closedToo)
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);

            int? parentId = !string.IsNullOrEmpty(node.Value) ? (int?) Convert.ToInt32(node.Value) : null;

            bool closedTooParam = !(closedToo == null || closedToo == false);

            IEnumerable<TreeViewModel> thelist = repository.AdminUnit.GetTreeElts(parentId.Value, user.DefaultHub.HubID);
            IEnumerable nodes = from item in thelist
                                // where item.ParentID == parentId || (parentId == null && item.ParentID == null)
                                group item by new {item.Value, item.Name, item.LoadOnDemand}
                                into itm
                                select new TreeViewItemModel
                                           {
                                               Text = itm.Key.Name + "( " + itm.Sum(l => l.Count) + " )",
                                               //item.Name g.Sum(b => b.QuantityInMT)
                                               Value = itm.Key.Value.ToString(),
                                               LoadOnDemand = true,
                                               //itm.Key.LoadOnDemand,
                                               Enabled = true
                                           };

            return new JsonResult {Data = nodes};
        }

        private int CountAllocationsUnder(int AdminUnitId)
        {

            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            var unclosed = (from dAll in repository.DispatchAllocation.GetAll()
                            where dAll.ShippingInstructionID.HasValue && dAll.ProjectCodeID.HasValue
                                  && user.DefaultHub.HubID == dAll.HubID && dAll.IsClosed == false
                            select dAll);

            BLL.AdminUnit adminUnit = repository.AdminUnit.FindById(AdminUnitId);

            if (adminUnit.AdminUnitType.AdminUnitTypeID == 2) //by region
                return
                    unclosed.Where(p => p.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == adminUnit.AdminUnitID).
                        Count();
            else if (adminUnit.AdminUnitType.AdminUnitTypeID == 3) //by zone
                return
                    unclosed.Where(p => p.FDP.AdminUnit.AdminUnit2.AdminUnitID == adminUnit.AdminUnitID).Count();
            else if (adminUnit.AdminUnitType.AdminUnitTypeID == 4) //by woreda
                return
                    unclosed.Where(p => p.FDP.AdminUnit.AdminUnitID == adminUnit.AdminUnitID).Count();
            else
                return 0;
        }

        public ActionResult GetZonesReport(int? AreaId)
        {
            if (AreaId.HasValue)
                return Json(new SelectList(repository.AdminUnit.GetZonesForReport(AreaId.Value), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
            else
                return Json(new SelectList(Enumerable.Empty<SelectListItem>(), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWoredasReport(int? ZoneId)
        {
            if (ZoneId.HasValue)
                return Json(new SelectList(repository.AdminUnit.GetWoredasForReport(ZoneId.Value), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
            else
                return Json(new SelectList(Enumerable.Empty<SelectListItem>(), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
        }

    }
}
