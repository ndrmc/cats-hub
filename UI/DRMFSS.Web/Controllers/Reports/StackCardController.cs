using System.Web.Mvc;

namespace DRMFSS.Web.Controllers.Reports
{
     [Authorize]
    public partial class StackCardController : BaseController
    {
        //
        // GET: /StackCard/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
