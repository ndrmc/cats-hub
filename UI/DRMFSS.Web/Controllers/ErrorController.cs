using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.Web.Models;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Controllers
{
    public partial class ErrorController : BaseController
    {
        private readonly IErrorService _ErrorService;

        public ErrorController(IErrorService errorServiceParam)
        {
            _ErrorService = errorServiceParam;
        }
        public virtual ActionResult NotFound(string url)
        {
            var originalUri = url ?? Request.QueryString["aspxerrorpath"] ?? Request.Url.OriginalString;

            var controllerName = (string)RouteData.Values["controller"];
            var actionName = (string)RouteData.Values["action"];
            var model = new NotFoundModel(new HttpException(404, "Failed to find page"), controllerName, actionName)
            {
                RequestedUrl = originalUri,
                ReferrerUrl = Request.UrlReferrer == null ? "" : Request.UrlReferrer.OriginalString
            };

            Response.StatusCode = 404;

            return View("NotFound", model);
        }
        //
        // GET: /Error/
        public virtual ActionResult ViewErrors()
        {
            
            //get all the errors here and paginate them for the admin/developer role
            IEnumerable<ErrorLog> errors = _ErrorService.GetAllError();
            return View(errors.ToList());
        }

       

        protected override void Dispose(bool disposing)
        {
            _ErrorService.Dispose();
            base.Dispose(disposing);
        }

    }
}
