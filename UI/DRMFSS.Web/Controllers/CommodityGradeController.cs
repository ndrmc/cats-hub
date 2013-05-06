using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
    [Authorize]
    public class CommodityGradeController : BaseController
    {
        private IUnitOfWork repository = new UnitOfWork();
        //
        // GET: /CommodityGrade/

        public ViewResult Index()
        {
            return View("Index", repository.CommodityGrade.GetAll().ToList());
        }

        public ActionResult Update()
        {
            return PartialView(repository.CommodityGrade.GetAll().ToList());
        }

        //
        // GET: /CommodityGrade/Details/5

        public ViewResult Details(int id)
        {
            CommodityGrade commoditygrade = repository.CommodityGrade.FindById(id);
            return View(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /CommodityGrade/Create

        [HttpPost]
        public ActionResult Create(CommodityGrade commoditygrade)
        {
            if (ModelState.IsValid)
            {
                repository.CommodityGrade.Add(commoditygrade);
                return Json(new { success = true }); 
            }

            return PartialView(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Edit/5

        public ActionResult Edit(int id)
        {
            CommodityGrade commoditygrade = repository.CommodityGrade.FindById(id);
            return PartialView(commoditygrade);
        }

        //
        // POST: /CommodityGrade/Edit/5

        [HttpPost]
        public ActionResult Edit(CommodityGrade commoditygrade)
        {
            if (ModelState.IsValid)
            {
                repository.CommodityGrade.SaveChanges(commoditygrade);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Delete/5

        public ActionResult Delete(int id)
        {
            CommodityGrade commoditygrade = repository.CommodityGrade.FindById(id);
            return View(commoditygrade);
        }

        //
        // POST: /CommodityGrade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CommodityGrade delCommodityGrade = repository.CommodityGrade.FindById(id);
            if (delCommodityGrade != null )
            {

                repository.CommodityGrade.DeleteByID(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Grade is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return this.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
           // db.Dispose();
            base.Dispose(disposing);
        }
    }
}