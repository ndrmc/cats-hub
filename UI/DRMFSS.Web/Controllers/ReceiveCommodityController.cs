using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Controllers
{
    public partial class ReceiveDetailController : BaseController
    {
        private CTSContext db = new CTSContext();
        private readonly IReceiveDetailService _receiveDetailService;
        private readonly ICommodityService _commodityService;
        private readonly ICommodityGradeService _commodityGradeService;
        private readonly IReceiveService _ReceiveService;
        private readonly IUnitService _unitService;

        public ReceiveDetailController(IReceiveDetailService receiveDetailService)
        {
            _receiveDetailService = receiveDetailService;
        }
        //
        // GET: /ReceiveDetail/

        public virtual ViewResult Index()
        {
            var receiveDetails = _receiveDetailService.Get(null, null, "Commodity,CommodityGrade,Receive,Unit");
            //var ReceiveDetails = db.ReceiveDetails.Include("Commodity").Include("CommodityGrade").Include("Receive").Include("Unit");
            return View(ReceiveDetails.ToList());
        }

       
        //
        // GET: /ReceiveDetail/Create

        public virtual ActionResult Create()
        {
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity().ToList(), "CommodityID", "Name");
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade().ToList(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_ReceiveService.GetAllReceive().ToList(), "ReceiveID", "SINumber");
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name");
            return View();
        } 

        //
        // POST: /ReceiveDetail/Create

        [HttpPost]
        public virtual ActionResult Create(ReceiveDetail ReceiveDetail)
        {
            if (ModelState.IsValid)
            {
                _receiveDetailService.AddReceiveDetail(ReceiveDetail);
                return RedirectToAction("Index");  
            }

            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity().ToList(), "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade().ToList(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_ReceiveService.GetAllReceive().ToList(), "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }
        
        //
        // GET: /ReceiveDetail/Edit/5

        public virtual ActionResult Edit(string id)
        {
            ReceiveDetail receiveDetail = _receiveDetailService.FindBy(r => r.ReceiveDetailID == Guid.Parse(id));


            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity().ToList(), "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade().ToList(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_ReceiveService.GetAllReceive().ToList(), "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        //
        // POST: /ReceiveDetail/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(ReceiveDetail ReceiveDetail)
        {
            if (ModelState.IsValid)
            {
                _receiveDetailService.EditReceiveDetail(ReceiveDetail);
                return RedirectToAction("Index");
            }

            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity().ToList(), "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade().ToList(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_ReceiveService.GetAllReceive().ToList(), "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}