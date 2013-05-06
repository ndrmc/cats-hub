using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
    public partial class ReceiveDetailController : BaseController
    {
        private DRMFSSEntities1 db = new DRMFSSEntities1();

        //
        // GET: /ReceiveDetail/

        public virtual ViewResult Index()
        {
            var ReceiveDetails = db.ReceiveDetails.Include("Commodity").Include("CommodityGrade").Include("Receive").Include("Unit");
            return View(ReceiveDetails.ToList());
        }

       
        //
        // GET: /ReceiveDetail/Create

        public virtual ActionResult Create()
        {
            ViewBag.CommodityID = new SelectList(db.Commodities, "CommodityID", "Name");
            ViewBag.CommodityGradeID = new SelectList(db.CommodityGrades, "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(db.Receives, "ReceiveID", "SINumber");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name");
            return View();
        } 

        //
        // POST: /ReceiveDetail/Create

        [HttpPost]
        public virtual ActionResult Create(ReceiveDetail ReceiveDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReceiveDetails.AddObject(ReceiveDetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CommodityID = new SelectList(db.Commodities, "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(db.CommodityGrades, "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(db.Receives, "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }
        
        //
        // GET: /ReceiveDetail/Edit/5

        public virtual ActionResult Edit(string id)
        {
            ReceiveDetail ReceiveDetail = db.ReceiveDetails.Single(r => r.ReceiveDetailID == Guid.Parse(id));
            ViewBag.CommodityID = new SelectList(db.Commodities, "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(db.CommodityGrades, "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(db.Receives, "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        //
        // POST: /ReceiveDetail/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(ReceiveDetail ReceiveDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReceiveDetails.Attach(ReceiveDetail);
                db.ObjectStateManager.ChangeObjectState(ReceiveDetail, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommodityID = new SelectList(db.Commodities, "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(db.CommodityGrades, "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(db.Receives, "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}