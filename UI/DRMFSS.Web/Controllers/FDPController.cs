using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;
using Telerik.Web.Mvc;
using System.Collections.Generic;
using DRMFSS.BLL.Repository;

namespace DRMFSS.Web.Controllers
{ 
    public class FDPController : BaseController
    {

        public FDPController(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public FDPController()
        {
           
        }

        //
        // GET: /FDP/

        public ViewResult Index()
        {

            ViewBag.Regions = repository.AdminUnit.GetRegions();
            var fdps = repository.FDP.GetAll();
            var list =
                fdps.OrderBy(f => f.AdminUnit.AdminUnit2.AdminUnit2.Name).ThenBy(f => f.AdminUnit.AdminUnit2.Name).
                    ThenBy(f => f.AdminUnit.Name).ThenBy(f => f.Name);
            return View("Index", list);
        }

        public ActionResult _FDPPartial()
        {

            ViewBag.Regions = repository.AdminUnit.GetRegions();
            var fdps = repository.FDP.GetAll();
            var list =
                fdps.OrderBy(f => f.AdminUnit.AdminUnit2.AdminUnit2.Name).ThenBy(f => f.AdminUnit.AdminUnit2.Name).
                    ThenBy(f => f.AdminUnit.Name).ThenBy(f => f.Name);
            return PartialView("Index", list);
        }

        public ActionResult IndexPartial()
        {

            ViewBag.Regions = repository.AdminUnit.GetRegions();
            var fdps = repository.FDP.GetAll();
            var list =
                fdps.OrderBy(f => f.AdminUnit.AdminUnit2.AdminUnit2.Name).ThenBy(f => f.AdminUnit.AdminUnit2.Name).
                    ThenBy(f => f.AdminUnit.Name).ThenBy(f => f.Name);
            return PartialView("IndexPartial", list);
        }

        public ActionResult Update()
        {
            ViewBag.Regions = repository.AdminUnit.GetRegions();
            var fdps = repository.FDP.GetAll();
            var list =
                fdps.OrderBy(f => f.AdminUnit.AdminUnit2.AdminUnit2.Name).ThenBy(f => f.AdminUnit.AdminUnit2.Name).
                    ThenBy(f => f.AdminUnit.Name).ThenBy(f => f.Name);
            return PartialView(list);
        }

        public ActionResult GetFDPs(int? woredaId)
        {
            // Check if the request came in a different Spelling than the parameter,
            if (woredaId == null && Request["WoredaID"] != null)
            {
                woredaId = System.Convert.ToInt32(Request["WoredaID"]);
            }

            if (woredaId != null)
            {
                var fdps = from p in repository.FDP.GetAll()
                           where p.AdminUnitID == woredaId
                           select new Models.AdminUnitItem() { Id = p.FDPID, Name = p.Name };
                return Json(new SelectList(fdps.OrderBy(f => f.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            return Json(new SelectList(new List<Models.AdminUnitItem>(),"Id","Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFDPsReport(int? woredaId)
        {
            if (woredaId != null)
            {
                var fdps = (from p in repository.FDP.GetAll()
                           where p.AdminUnitID == woredaId
                           select new Models.AdminUnitItem() { Id = p.FDPID, Name = p.Name }).ToList();
                fdps.Insert(0, new Models.AdminUnitItem { Name = "All" });
                return Json(new SelectList(fdps.OrderBy(f => f.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            return Json(new SelectList(new List<Models.AdminUnitItem>(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        [GridAction(EnableCustomBinding = false)]
        public ActionResult GetFDPGrid(int? woredaId, int? zoneId, int? regionId)
        {
            List<BLL.FDP> fdps = new List<BLL.FDP>();
            if (woredaId.HasValue)
            {
                fdps = repository.FDP.GetFDPsByWoreda(woredaId.Value);
            }
            else if (zoneId.HasValue)
            {
                fdps = repository.FDP.GetFDPsByZone(zoneId.Value);
            }
            else if (regionId.HasValue)
            {
                fdps = repository.FDP.GetFDPsByRegion(regionId.Value);
            }else
            {
                fdps = repository.FDP.GetAll();
            }
            var f = from p in fdps
                    select new
                    {
                        FDPID = p.FDPID,
                        Name = p.Name,
                        NameAM = p.NameAM,
                        AdminUnit = new
                        {
                            Name = p.AdminUnit.Name,
                            AdminUnit2 = new
                            {
                                Name = p.AdminUnit.AdminUnit2.Name,
                                AdminUnit2 = new 
                                {
                                    Name = p.AdminUnit.AdminUnit2.AdminUnit2.Name
                                }
                            }
                        }
                    };
           var result =  f.OrderBy(o => o.AdminUnit.AdminUnit2.AdminUnit2.Name).ThenBy(o => o.AdminUnit.AdminUnit2.Name).ThenBy(
                o => o.AdminUnit.Name).ThenBy(o => o.Name);
            return View(new GridModel(result));
        }

        //
        // GET: /FDP/Details/5

        public ViewResult Details(int id)
        {
            FDP fdp = repository.FDP.FindById(id);
            return View(fdp);
        }

        //
        // GET: /FDP/Create

        public ActionResult Create(int woredaId)
        {
            Models.AdminUnitModel model = new Models.AdminUnitModel();
            BLL.AdminUnit woreda = repository.AdminUnit.FindById(woredaId);
            model.SelectedWoredaId = woreda.AdminUnitID;
            model.SelectedWoredaName = woreda.Name;
            model.SelectedZoneId = woreda.AdminUnit2.AdminUnitID;
            model.SelectedZoneName = woreda.AdminUnit2.Name;
            model.SelectedRegionId = woreda.AdminUnit2.AdminUnit2.AdminUnitID;
            model.SelectedRegionName = woreda.AdminUnit2.AdminUnit2.Name;

            return PartialView("Create", model);
        } 

        //
        // POST: /FDP/Create

        [HttpPost]
        public ActionResult Create(Models.AdminUnitModel unit)
        {
            if (ModelState.IsValid)
            {
                BLL.FDP fdp = new FDP()
                                  {AdminUnitID = unit.SelectedWoredaId, Name = unit.UnitName, NameAM = unit.UnitNameAM};
                if (ModelState.IsValid)
                {
                    repository.FDP.Add(fdp);
                    return Json(new {success = true});
                }
            }
            // ViewBag.AdminUnitID = new SelectList(new BLL.Repository.AdminUnitRepository().GetAllWoredas(), "AdminUnitID", "Name", fdp.AdminUnitID);
            return PartialView("Create", unit);
        }
        
        //
        // GET: /FDP/Edit/5
 
        public ActionResult Edit(int id)
        {
            FDP fdp = repository.FDP.FindById(id);
           // ViewBag.AdminUnitID = new SelectList(db.AdminUnits, "AdminUnitID", "Name", fdp.AdminUnitID);
            return PartialView(fdp);
        }

        //
        // POST: /FDP/Edit/5

        [HttpPost]
        public ActionResult Edit(FDP fdp)
        {
            if (ModelState.IsValid)
            {
                repository.FDP.SaveChanges(fdp);
                return Json(new { success = true });
            }
            //ViewBag.AdminUnitID = new SelectList(db.AdminUnits, "AdminUnitID", "Name", fdp.AdminUnitID);
            return Json(new { success = false});
        }

        //
        // GET: /FDP/Delete/5
 
        public ActionResult Delete(int id)
        {
            FDP fdp = repository.FDP.FindById(id);
            return View("Delete", fdp);
        }

        //
        // POST: /FDP/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.FDP.DeleteByID(id);
            return RedirectToAction("Index");
        }
    }
}
