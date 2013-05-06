using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using DRMFSS.BLL;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;


namespace DRMFSS.Web.Controllers
{   
    [Authorize]
    public class CommodityController : BaseController
    {
        //
        // GET: /Commodity/
        public CommodityController()
        {
       
        }

        public CommodityController(IUnitOfWork _repository)
        {
            this.repository = _repository;
        }

        //public CommodityController(ICommodityRepository commodityRepository,
        //    ICommodityTypeRepository commodityTypeRepository)
        //{
        //    this.commodityRepository = commodityRepository;
        //    this.commodityTypeRepository = commodityTypeRepository;
        //}

        public ActionResult Index()
        {
            ViewBag.CommodityTypes = repository.CommodityType.GetAll();

            var parents = repository.Commodity.GetAllParents().OrderBy(o=>o.Name);
            if (parents != null)
                ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name");
            else
                ViewBag.ParentID = new SelectList(new SelectList(Enumerable.Empty<SelectListItem>(), "CommodityID", "Name")); 
    
            var firstOrDefault =
                repository.Commodity.GetAllParents() == null ? null : repository.Commodity.GetAllParents().FirstOrDefault();
            
            if (firstOrDefault != null)
                ViewBag.SelectedCommodityID = firstOrDefault.CommodityID;
            else
                ViewBag.SelectedCommodityID = 1;
          
            ViewBag.Parents = parents;

            var commReturn = repository.Commodity.GetAll() == null ? Enumerable.Empty<Commodity>() : repository.Commodity.GetAll().ToList();

            return View(commReturn);
        
        }
        
        public ActionResult CommodityListPartial()
        {
            // Default to food commodities
            int commodityTypeId = 1;
            if(Request["type"] != null)
            {
                commodityTypeId = Convert.ToInt32(Request["type"]);    
            }
            
            ViewBag.ShowParentCommodity = false;
            var parents = repository.Commodity.GetAllParents().Where(o=>o.CommodityTypeID == commodityTypeId).OrderBy(o => o.Name);
            var firstOrDefault =
                parents == null ? null : parents.FirstOrDefault();

            if (firstOrDefault != null)
                ViewBag.SelectedCommodityID = firstOrDefault.CommodityID;
            else
                ViewBag.SelectedCommodityID = 1;
            
            ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name");
            return PartialView("_CommodityPartial",parents);
        }

        public ActionResult SubCommodityListPartial(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            ViewBag.ShowParentCommodity = true;
                ViewBag.SelectedCommodityID = id;

                return PartialView("_CommodityPartial",
                                   repository.Commodity.GetAllSubCommoditiesByParantId(id.Value).OrderBy(o => o.Name));
        }

        public ActionResult GetParentList()
        {
            var parents = from listItem in repository.Commodity.GetAllParents()
                        select new Commodity()
                        {
                            CommodityID = listItem.CommodityID,
                            Name = listItem.Name
                        };
            return Json(new SelectList(parents.OrderBy(o => o.Name), "CommodityID", "Name"), JsonRequestBehavior.AllowGet);
        }

        /**
         * <param>
         * //if param is null (i.e. item.ParentID == null ) it's a parent element /else it's a child
         * </param>
         */

        public ActionResult Update(int? param)
        {

            
            ViewBag.index = param != null ? 1 : 0;

            var parents = repository.Commodity.GetAllParents();
            if(param != null) 
            {
                //for rendering the subCommodityList accordingly 
                ViewBag.SelectedCommodityID = param; 
                ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name", param);
            }
            else //it's a child commodity
            {
                var firstOrDefault =
                    repository.Commodity.GetAllParents() == null ? null : repository.Commodity.GetAllParents().FirstOrDefault();

                if (firstOrDefault != null)
                    ViewBag.SelectedCommodityID = firstOrDefault.CommodityID;
                else
                    ViewBag.SelectedCommodityID = 1;


                ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name");
            }
            
            
            ViewBag.Parents = parents;
            return PartialView(repository.Commodity.GetAll().OrderBy(o => o.Name));
        }
        //
        // GET: /Commodity/Details/5

        public ViewResult Details(int id)
        {
            Commodity commodity = repository.Commodity.FindById(id);
            return View("Details",commodity);
        }
        /**
         * <type> indicates the type of commodity to be (parent/child)</type>
         * <Parent> An Optional Nullable value param for preseting the sub Commoditites to be created</Praent>
         */
        //
        // GET: /Commodity/Create

        public ActionResult Create(int type, int? Parent)
        {
            //TODO  validation check @ the post server side if the user mischively sent a non-null parent 
            //this check should also be done for the editing part
            
            if (0 == type)
            {
                ViewBag.ParentID = new SelectList(repository.Commodity.GetAllParents(), "CommodityID", "Name", Parent);

                //drop down boxes don't remove thses cos i used them to set a hidden value
                
                var firstOrDefault =
                    repository.Commodity.GetAllParents() == null ? null : repository.Commodity.GetAllParents().FirstOrDefault(p => p.CommodityID == Parent);
                
                if (firstOrDefault != null){
                    ViewBag.CommodityTypeID = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name",
                                                             firstOrDefault.CommodityTypeID);
                }
                else
                {    
                   //TODO null value validation can be set here later 
                    ViewBag.CommodityTypeID = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name");
                }

                //disabled text boxes (elias worte this part i think)
                ViewBag.isParent = true;
                
                if (Parent != null)
                {
                    var commodity = repository.Commodity.FindById(Parent.Value);
                    if ((commodity.CommodityType != null) && (commodity != null)){
                        ViewBag.CommodityType = commodity.CommodityType.Name;
                        ViewBag.ParentCommodity = commodity.Name;
                        ViewBag.SelectedCommodityID = commodity.CommodityID;
                    }
                    else
                    {
                        ViewBag.CommodityType = "";
                        ViewBag.ParentCommodity = "";
                    }
                }
          
            }

            else
            {
                ViewBag.CommodityTypeID = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name");
                ViewBag.isParent = false;
            }
                
            var partialCommodity = new Commodity();
            
            var partialParent =
                repository.Commodity.GetAllParents() == null ? null :
                repository.Commodity.GetAllParents().FirstOrDefault(p => p.CommodityID == Parent);
           
            if (partialParent != null)
                {
                    partialCommodity.CommodityTypeID = partialParent.CommodityTypeID;
                }
            
            //TODO either ways it will be null, but we can make this assignment to null/and move it in side else after testing
            //to be sure for preventing hierarchy(tree)
            partialCommodity.ParentID = Parent;
            
            return PartialView(partialCommodity);
        }

        //
        // POST: /Commodity/Create

        [HttpPost]
        public ActionResult Create(Commodity commodity)
        {
            if(!repository.Commodity.IsCodeValid(commodity.CommodityID,commodity.CommodityCode))
            {
                ModelState.AddModelError("CommodityCode",@"Commodity Code should be unique.");
            }
            if (!repository.Commodity.IsNameValid(commodity.CommodityID, commodity.Name))
            {
                ModelState.AddModelError("Name", @"Commodity Name should be unique.");
            }

            if (ModelState.IsValid)
            {
                repository.Commodity.Add(commodity);
                return Json(new { success = true }); 
            }

            this.Create(commodity.ParentID != null ? 0 : 1, commodity.ParentID);
            return PartialView(commodity);
        }

        //
        // GET: /Commodity/Edit/5

        //TODO  validation check @ the post server side if the user mischively sent a non-null parent 
        //this check should also be done for the creating part

        public ActionResult Edit(int id)
        {
            Commodity commodity = repository.Commodity.FindById(id);
            var commodities = repository.Commodity.GetAll();
            
            //this node is already a parent(i.e. if we can find at least one record with this id as a parent) 
            if (commodity != null && ((repository.Commodity.GetAllSubCommoditiesByParantId(id).Count() != 0) || commodity.ParentID == null))
            {
                ViewBag.ParentID = new SelectList(commodities.DefaultIfEmpty().Where(c => c.CommodityID == -1), "CommodityID", "Name", commodity.ParentID);
                ViewBag.CommodityTypeID = new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name", commodity.CommodityTypeID);
                ViewBag.ShowParentCommodity = false;
                ViewBag.isParent = false;
   
            }
            //they must be parents with no parents (i.e. parents with value ParentID = null ) 
            // and 
            //they should not be Parents to them selves (i.e. self referencing is not allowed)
            else
            {
                if (commodity != null)
                {
                    ViewBag.ParentID =
                        new SelectList(commodities.Where(c => c.ParentID == null && c.CommodityID != id),
                                       "CommodityID", "Name", commodity.ParentID);
                    ViewBag.CommodityTypeID =
                        new SelectList(repository.CommodityType.GetAll(), "CommodityTypeID", "Name", commodity.CommodityTypeID);

                    ViewBag.CommodityType = commodity.CommodityType.Name;
                    ViewBag.ParentCommodity = commodity.Commodity2.Name;
                }
              
                ViewBag.ShowParentCommodity = true;
                ViewBag.isParent = true;
 
              
                
            }
            //ViewBag.CommodityTypeID = new SelectList(db.CommodityTypes, "CommodityTypeID", "Name", commodity.CommodityTypeID);
            return PartialView(commodity);
        }


        public ActionResult ParentCommodities(int CommodityTypeID)
        {
            var commodities = from v in repository.CommodityType.FindById(CommodityTypeID).Commodities
                              where v.ParentID == null
                              select v;
            commodities = commodities.OrderBy(o => o.Name);
            return Json(new SelectList(commodities, "CommodityID", "Name", commodities.FirstOrDefault().CommodityID));
        }
        //
        // POST: /Commodity/Edit/5

        [HttpPost]
        public ActionResult Edit(Commodity commodity)
        {
            // TODO: move this to a shared helper function.
            if (!repository.Commodity.IsCodeValid(commodity.CommodityID, commodity.CommodityCode))
            {
                ModelState.AddModelError("CommodityCode", @"Commodity Code should be unique.");
            }
            if (!repository.Commodity.IsNameValid(commodity.CommodityID, commodity.Name))
            {
                ModelState.AddModelError("Name", @"Commodity Name should be unique.");
            }

            if (ModelState.IsValid)
            {
                repository.Commodity.SaveChanges(commodity);
                return Json(new { success = true });
            }
            this.Edit(commodity.CommodityID);
            return PartialView(commodity);
        }

        //
        // GET: /Commodity/Delete/5

        public ActionResult Delete(int id)
        {
            var delCommodity = repository.Commodity.FindById(id);
            return View("Delete",delCommodity);
        }

        //
        // POST: /Commodity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            var delCommodity = repository.Commodity.FindById(id);
            var countOfChildren = repository.Commodity.GetAllSubCommodities().Count(p => p.ParentID == id); 
            
            if (delCommodity != null &&
                (countOfChildren == 0) &&
                delCommodity.ReceiveDetails.Count == 0 &&
                delCommodity.DispatchDetails.Count == 0 &&
                delCommodity.DispatchAllocations.Count == 0  &&
                delCommodity.GiftCertificateDetails.Count == 0)
            {

                repository.Commodity.DeleteByID(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return View("Delete", delCommodity); //this.Delete(id);
            
        }
        public ActionResult CommodityParentListByType(int? CommodityTypeID, int? editModeVal)
        {
            if (CommodityTypeID != null)
            {
                var comms =
                    repository.Commodity.GetAllParents().Where(p => p.CommodityTypeID == CommodityTypeID).ToList();
                return Json(new SelectList(comms, "CommodityID", "Name", editModeVal), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new EmptyResult());
            }
        }

        public ActionResult CommodityListByType(int? CommodityTypeID, int? editModeVal, string SINumber, int? CommoditySourceID)
        {

            ArrayList optGroupedList = new ArrayList();
            if (CommodityTypeID != null)
            {
                    List<Commodity> Parents = new List<Commodity>();
                    Parents = repository.Commodity.GetAllParents().Where(p => p.CommodityTypeID == CommodityTypeID).ToList();

                    foreach (Commodity Parent in Parents)
                    {
                        var subCommodities = Parent.Commodity1; 
                        optGroupedList.Add(
                                new { Value = Parent.CommodityID, Text = Parent.Name, unselectable = false, id = Parent.ParentID });
                        
                        if (subCommodities != null) //only if it has a subCommodity
                        {
                            foreach (Commodity subCommodity in subCommodities)
                            {
                                optGroupedList.Add(
                                    new
                                    {
                                        Value = subCommodity.CommodityID,
                                        Text = subCommodity.Name,
                                        unselectable = true,
                                        id = subCommodity.ParentID
                                    });
                            }
                        }
                    }
               return Json(optGroupedList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new EmptyResult());
            }
        }

        public JsonResult IsCodeValid(int? CommodityID, string CommodityCode)
        {
            return Json(repository.Commodity.IsCodeValid(CommodityID, CommodityCode),JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameValid(int?CommodityID, string Name)
        {
            return Json(repository.Commodity.IsNameValid(CommodityID, Name), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
           // CommodityRepo.Dispose();//we should despose the CommodityRepo Oject here
            base.Dispose(disposing);
        }
    }
}