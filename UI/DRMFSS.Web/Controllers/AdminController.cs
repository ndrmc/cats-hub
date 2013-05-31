using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.Shared;
using System.Web.Security;

namespace DRMFSS.Web.Controllers
{
    public partial class AdminController : BaseController
    {
        CTSContext db = new CTSContext();
        IUnitOfWork repository = new BLL.UnitOfWork();
        public virtual ViewResult Index()
        {
            var userProfiles = repository.UserProfile.GetAll();
            return View("Users/Index", userProfiles.OrderBy(u => u.UserName).ToList());
        }

        public virtual ActionResult Update()
        {
            var userProfiles = repository.UserProfile.GetAll();
            return PartialView("Users/Update", userProfiles.OrderBy(u => u.UserName).ToList());
        }

        public virtual ViewResult Home()
        {   
            return View("Users/Home");
        }

        public virtual ViewResult Details(int id)
        {
            UserProfile userprofile = repository.UserProfile.FindById(id); 
            return View("Users/Details", userprofile);
        }

        public virtual ActionResult Create()
        {
            ViewBag.UserRoles = new SelectList(repository.UserRole.GetAll(), "UserRoleID", "UserRoleID");
            return PartialView("Users/Create");
        } 

        //
        // POST: /Admin/Create

        [HttpPost]
        public virtual ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                userprofile.Password = MD5Hashing.MD5Hash(userprofile.Password);
                repository.UserProfile.Add(userprofile);
                return Json(new { success = true });  
            }

            ViewBag.UserProfileID = new SelectList(repository.UserRole.GetAll(), "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Create", userprofile);
        }
        
        //
        // GET: /Admin/Edit/5

        public virtual ActionResult Edit(int id)
        {
            UserProfile userprofile = repository.UserProfile.FindById(id);
            //ViewData["roles"] = new SelectList(db.Roles, "RoleID", "Name", userprofile.UserRole.RoleID);
            //ViewBag.UserProfileID = new SelectList(db.Roles, "RoleID", "Name", userprofile.UserRole.RoleID);
            Session["SELECTEDUSER"] = userprofile;
            return PartialView("Users/Edit", userprofile);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(UserProfile userprofile)
        {
            BLL.UserProfile cachedProfile = Session["SELECTEDUSER"] as BLL.UserProfile;
            this.ModelState.Remove("Password");
            if (ModelState.IsValid && cachedProfile != null)
            {
                userprofile.Password = cachedProfile.Password;
                db.UserProfiles.Attach(userprofile);
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                Session.Remove("SELECTEDUSER");
                return Json(new { success = true }); ;
            }
            ViewBag.UserProfileID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Edit", userprofile);
        }

        //
        // GET: /Admin/Delete/5

        public virtual ActionResult Delete(int id)
        {
            UserProfile userprofile = db.UserProfiles.Single(u => u.UserProfileID == id);
            return View("Users/Delete", userprofile);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {            
            UserProfile userprofile = db.UserProfiles.Single(u => u.UserProfileID == id);
            db.UserProfiles.Remove(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public virtual ActionResult UserRoles(string userName)
        {
            Models.UserRolesModel userroles = new Models.UserRolesModel();
            userroles.UserRoles = GetUserRoles(userName).OrderBy(o=>o.SortOrder).ToArray();
            Session["Roles"] = userroles;
            Session["UserName"] = userName;
            return PartialView("Users/UserRoles", userroles);
        }

        public virtual ActionResult UserHubs(string userName)
        {
            Models.UserHubsModel userhubs = new Models.UserHubsModel();
            userhubs.UserHubs = GetUserHubs(userName).OrderBy(o => o.Name).ToList();
            Session["Hubs"] = userhubs;
            Session["UserName"] = userName;
            return PartialView( "Users/UserHubs", userhubs);
        }

        [HttpPost]
        public virtual ActionResult UserHubs(FormCollection userHubs)
        {
            Models.UserHubsModel hubModel = Session["Hubs"] as Models.UserHubsModel;
            string userName = Session["UserName"].ToString();
            for (int i = 0; i < hubModel.UserHubs.Count(); i++)
            {
                Models.UserHubModel model = new Models.UserHubModel();
                model.HubID = hubModel.UserHubs[i].HubID;
                model.Name = hubModel.UserHubs[i].Name;
                model.Selected = userHubs.GetValue(string.Format("[{0}].Selected", model.HubID)).AttemptedValue.Contains("true");
                if (model.Selected != hubModel.UserHubs[i].Selected)
                {
                    int userID = (from v in db.UserProfiles
                                 where v.UserName == userName
                                 select v.UserProfileID).FirstOrDefault();

                    BLL.Hub hub = new Hub();
                    if (model.Selected)
                    {
                        hub.AddUser(model.HubID,userID);
                    }else
                    {
                        hub.RemoveUser(model.HubID,userID);
                    }
                      
                }

            }
            return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult UserRoles(FormCollection userRoles)
        {
            
            Models.UserRolesModel roleModel = Session["Roles"] as Models.UserRolesModel;
            string userName = Session["UserName"].ToString();
            for (int i = 0; i < roleModel.UserRoles.Count(); i++)
            {
                Models.UserRoleModel model = new Models.UserRoleModel();
                model.RoleId = roleModel.UserRoles[i].RoleId;
                model.RoleName = roleModel.UserRoles[i].RoleName;
                model.Selected = userRoles.GetValue(string.Format("[{0}].Selected", model.RoleId)).AttemptedValue.Contains("true");
                if (model.Selected != roleModel.UserRoles[i].Selected)
                {
                    if(model.Selected)
                    new BLL.Role().AddUserToRole(model.RoleId,userName);
                    else
                        new BLL.Role().RemoveRole(model.RoleName,userName);
                }
                
            }
            return Json(new { success = true }); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private List<Models.UserRoleModel> GetUserRoles(string userName)
        {
            string[] roles = Roles.GetRolesForUser(userName);
            BLL.CTSContext entities = new BLL.CTSContext();
            var userRoles = from role in entities.Roles
                            select new Models.UserRoleModel() { RoleId = role.RoleID, RoleName = role.Name, Selected = roles.Contains(role.Name),SortOrder = role.SortOrder};
            return userRoles.ToList();
        }

        private List<Models.UserHubModel> GetUserHubs(string userName)
        {
            var warehouses = from v in db.UserHubs
                             where v.UserProfile.UserName == userName
                             select v.HubID;
           
            var userHubs = from v in db.Hubs
                            select new Models.UserHubModel() { HubID = v.HubID, Name = v.Name + " : " + v.HubOwner.Name, Selected = warehouses.Contains(v.HubID) } ;
            return userHubs.ToList();
        }
    }
}
