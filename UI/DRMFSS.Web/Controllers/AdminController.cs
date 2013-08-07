using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;
using DRMFSS.Shared;
using System.Web.Security;
using DRMFSS.Web.Models;

namespace DRMFSS.Web.Controllers
{
    public partial class AdminController : BaseController
    {
        //CTSContext db = new CTSContext();
        //IUnitOfWork repository = new BLL.UnitOfWork();
        private readonly IUserProfileService _userProfileService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly IUserHubService _userHubService;
        private readonly IHubService _hubService;

        public AdminController(IUserProfileService userProfileService, IUserRoleService userRoleService, IRoleService roleService, IUserHubService userHubService, IHubService hubService)
        {
            this._userProfileService = userProfileService;
            this._userRoleService = userRoleService;
            this._roleService = roleService;
            this._userHubService = userHubService;
            this._hubService = hubService;
        }

        public virtual ViewResult Index()
        {
            var userProfiles = _userProfileService.Get(null,t=>t.OrderBy(o=>o.UserName)).ToList();
            return View("Users/Index", userProfiles);
        }

        public virtual ActionResult Update()
        {
            var userProfiles = _userProfileService.Get(null, t => t.OrderBy(o => o.UserName)).ToList();
            return PartialView("Users/Update", userProfiles);
        }

        public virtual ViewResult Home()
        {   
            return View("Users/Home");
        }

        public virtual ViewResult Details(int id)
        {
            var userprofile = _userProfileService.FindById(id);
            return View("Users/Details", userprofile);
        }

        public virtual ActionResult Create()
        {
            ViewBag.UserRoles = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID");
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
                _userProfileService.AddUserProfile(userprofile);
                return Json(new { success = true });  
            }

            ViewBag.UserProfileID = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Create", userprofile);
        }
        
        //
        // GET: /Admin/Edit/5

        public virtual ActionResult Edit(int id)
        {
            UserProfile userprofile = _userProfileService.FindById(id);
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
            var cachedProfile = Session["SELECTEDUSER"] as BLL.UserProfile;
            this.ModelState.Remove("Password");
            if (ModelState.IsValid && cachedProfile != null)
            {
                userprofile.Password = cachedProfile.Password;
                _userProfileService.EditUserProfile(userprofile);
                Session.Remove("SELECTEDUSER");
                return Json(new { success = true }); ;
            }
            ViewBag.UserProfileID = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Edit", userprofile);
        }

        //
        // GET: /Admin/Delete/5

        public virtual ActionResult Delete(int id)
        {
            UserProfile userprofile = _userProfileService.FindById(id);
            return View("Users/Delete", userprofile);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {   
            _userProfileService.DeleteById(id);
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
                    int userID = (from v in _userProfileService.GetAllUserProfile()
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
            _userProfileService.Dispose();
            _userRoleService.Dispose();
            _userHubService.Dispose();
            _hubService.Dispose();
            _roleService.Dispose();
            base.Dispose(disposing);
        }

        private IEnumerable<UserRoleModel> GetUserRoles(string userName)
        {
            string[] roles = Roles.GetRolesForUser(userName);
            var userRoles = from role in _roleService.GetAllRole()
                            select new Models.UserRoleModel() { RoleId = role.RoleID, RoleName = role.Name, Selected = roles.Contains(role.Name),SortOrder = role.SortOrder};
            return userRoles.ToList();
        }

        private IEnumerable<UserHubModel> GetUserHubs(string userName)
        {
            
            var warehouses = from v in _userHubService.GetAllUserHub()
                             where v.UserProfile.UserName == userName
                             select v.HubID;
           
            var userHubs = from v in _hubService.GetAllHub()
                            select new Models.UserHubModel() { HubID = v.HubID, Name = v.Name + " : " + v.HubOwner.Name, Selected = warehouses.Contains(v.HubID) } ;
            return userHubs.ToList();
        }
    }
}
