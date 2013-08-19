using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using System.Data;
using DRMFSS.Web.Models;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Controllers
{
    public class RolesController : BaseController
    {
        
        private CTSContext db = new CTSContext();
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleSerivce)
        {
            _roleService = roleSerivce;
        }
        //
        // GET: /Admin/


        //[AutoMap(typeof(BLL.Role),typeof(DRMFSS.Web.Models.RoleModel))]
        public ViewResult Index()
        {
            var roles =  _roleService.GetAllRole().OrderBy(r=>r.SortOrder).ToList();
            return View("Index",roles);
            //var roles = db.Roles;
            //return View("Index", roles.OrderBy(r => r.SortOrder).ToList());
        }

        public ActionResult Update()
        {
            return PartialView(_roleService.GetAllRole().OrderBy(r=>r.SortOrder)).ToList();
        }

        //
        // GET: /Admin/Details/5
        //[AutoMap(typeof(BLL.Role), typeof(DRMFSS.Web.Models.RoleModel))]
        public ViewResult Details(int id)
        {
            Role role = _roleService.FindBy(r => r.RoleID == id);
            return View("Details", role);

            //Role role = db.Roles.Single(u => u.RoleID == id);
            //return View("Details", role);
        }

        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_roleService.GetAllRole().ToList(), "RoleID", "RoleID");
            return PartialView("Create");
        }
        public bool testing(Role role)
        {
            return TryValidateModel(role);
        }

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)//testing(role))//
            {
                _roleService.AddRole(role);
                return Json(new { success = true }); 
            }

            ViewBag.UserProfileID = new SelectList(db.Roles, "RoleID", "RoleID", role.RoleID);
            return PartialView(role);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            Role role = _roleService.FindBy(r => r.RoleID == id);
            
            ViewBag.UserProfileID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", role.RoleID);
            return PartialView("Edit", role);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleService.EditRole(role);
                
                return Json(new { success = true }); 
            }
            ViewBag.UserProfileID = new SelectList(db.Roles, "RoleID", "RoleID", role.RoleID);
            return PartialView("Edit", role);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            Role role = _roleService.FindBy(r => r.RoleID == id);
            
            return View(role);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _roleService.FindBy(r => r.RoleID == id);

            _roleService.DeleteRole(role);
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
         
    }
}
