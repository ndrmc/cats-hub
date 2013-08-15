using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Controllers
{
     [Authorize]
    public class CurrentHubController : BaseController
    {
         private readonly IUserProfileService _userProfileService;
        //
        // GET: /CurrentWarehouse/
         public CurrentHubController(IUserProfileService userProfileService)
         {
             _userProfileService = userProfileService;
         }

        public virtual ActionResult DisplayCurrentHub()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            return PartialView("DisplayHub", new Models.CurrentUserModel(user)); 
        }


        public UserProfile CurrentUser(string user)
        {
            
            return _userProfileService.GetUser(user);
        }

        public virtual ActionResult HubList()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            return user != null ? PartialView("SelectHub", new Models.CurrentUserModel(user)) : null;
        }




        public virtual ActionResult ChangeHub(Models.CurrentUserModel currentUser)
        {

            var user = _userProfileService.GetUser(User.Identity.Name);
            user.ChangeHub(currentUser.DefaultHubId);
            
            //return RedirectToAction("Index", "Dispatch");
            //return Json(new { success = true });
            return Request.UrlReferrer != null ? Redirect(Request.UrlReferrer.PathAndQuery) : null;
        }

    }
}
