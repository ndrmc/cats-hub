using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Contexts;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
     [Authorize]
    public partial class CurrentHubController : BaseController
    {
        //
        // GET: /CurrentWarehouse/
         IUnitOfWork repository = new UnitOfWork();
         public CurrentHubController()
         {

         }

        public virtual ActionResult DisplayCurrentHub()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            return PartialView("DisplayHub", new Models.CurrentUserModel(user)); 
        }


        public BLL.UserProfile CurrentUser(string user)
        {
            
            return repository.UserProfile.GetUser(user);
        }

        public virtual ActionResult HubList()
        {
            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            if (user != null)
            {
                return PartialView("SelectHub", new Models.CurrentUserModel(user));
            }
            return null;
        }




        public virtual ActionResult ChangeHub(Models.CurrentUserModel currentUser)
        {

            BLL.UserProfile user = repository.UserProfile.GetUser(User.Identity.Name);
            user.ChangeHub(currentUser.DefaultHubId);
            
            //return RedirectToAction("Index", "Dispatch");
            //return Json(new { success = true });
            return Redirect(this.Request.UrlReferrer.PathAndQuery);
        }

    }
}
