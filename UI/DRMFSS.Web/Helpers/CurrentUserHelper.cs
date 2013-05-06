using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Helpers
{
    public static class CurrentUserHelper
    {
        public static UserProfile GetCurrentUser(this HtmlHelper helper)
        {
           if( helper.ViewContext.HttpContext.User.Identity.IsAuthenticated)
           {
               return ((BaseController) helper.ViewContext.Controller).UserProfile;
               //IUnitOfWork repository = new UnitOfWork();
               //return repository.UserProfile.GetUser(helper.ViewContext.HttpContext.User.Identity.Name);
           }
           return null;
        }

        
    }
}