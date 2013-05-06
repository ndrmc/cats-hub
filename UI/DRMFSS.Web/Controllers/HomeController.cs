using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.Web.Models;
using DRMFSS.BLL;
using System.Text;
using System.Security.Cryptography;


namespace DRMFSS.Web.Controllers
{
    
    public class HomeController : BaseController
    {
       public ActionResult Index()
       {
           return View();
       }
      
    }
}
