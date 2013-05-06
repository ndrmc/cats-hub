using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers.Utilities
{
    public class TranslationController : BaseController
    {
        //
        // GET: /Translation/

        public ActionResult Index()
        {
           
            return View(repository.Translation.GetAll("am"));
        }

        public ActionResult Edit(int id)
        {
            return PartialView(repository.Translation.FindById(id));
        }

        [HttpPost]
        public ActionResult Save( Translation model)
        {
            Translation translation = repository.Translation.FindById(model.TranslationID);
            translation.Phrase = translation.Phrase.Trim();
            translation.TranslatedText = model.TranslatedText.Trim();
            repository.Translation.SaveChanges(translation);
            return RedirectToAction("Index");
        }
    }
}
