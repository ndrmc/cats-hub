using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
    public class LetterTemplateController : BaseController
    {
        private IUnitOfWork repositories = new UnitOfWork();

        //
        // GET: /LetterTemplate/

        public ViewResult Index()
        {
            return View(repositories.LetterTemplate.GetAll());
        }

        //
        // GET: /LetterTemplate/Details/5

        public ViewResult Details(int id)
        {
            LetterTemplate lettertemplate = repositories.LetterTemplate.FindById(id);
            lettertemplate.Template = Server.HtmlDecode(lettertemplate.Template);
            return View(lettertemplate);
        }

        //
        // GET: /LetterTemplate/Create

        public ActionResult Create()
        {
            BLL.LetterTemplate template = new LetterTemplate();
            //template.Template = new Helpers.LetterTemplateHelper().GetDefaultGiftDetail();
            return View(template);
        } 

        //
        // POST: /LetterTemplate/Create

        [HttpPost]
        public ActionResult Create(LetterTemplate lettertemplate)
        {
            if (ModelState.IsValid)
            {
                repositories.LetterTemplate.Add(lettertemplate);
                return RedirectToAction("Index");  
            }

            return View(lettertemplate);
        }
        
        //
        // GET: /LetterTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            LetterTemplate lettertemplate = repositories.LetterTemplate.FindById(id);
            lettertemplate.Template = Server.HtmlDecode(lettertemplate.Template);
            return View(lettertemplate);
        }

        //
        // POST: /LetterTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(LetterTemplate lettertemplate)
        {
            if (ModelState.IsValid)
            {
                repositories.LetterTemplate.SaveChanges(lettertemplate);
                return RedirectToAction("Index");
            }
            return View(lettertemplate);
        }

        //
        // GET: /LetterTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            LetterTemplate lettertemplate = repositories.LetterTemplate.FindById(id);
            return View(lettertemplate);
        }

        //
        // POST: /LetterTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repositories.LetterTemplate.DeleteByID(id);
            return RedirectToAction("Index");
        }


        public ActionResult SelectPrintTemplate(int certificateId)
        {
            List<BLL.LetterTemplate> templates = repositories.LetterTemplate.GetAll();
            ViewBag.Templates = new SelectList(templates.OrderBy(p => p.Name),"LetterTemplateID", "Name");
            Models.PrintCertificateModel model = new Models.PrintCertificateModel();
            model.SelectedCertificateId = certificateId;
            return PartialView("SelectTemplatePartial", model);
        }

        [HttpPost]
        public ActionResult SelectPrintTemplate(Models.PrintCertificateModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LetterPreview",new {certificateId = model.SelectedCertificateId, templateId = model.SelctedTemplateId});
            }
            return View();
        }


        public ActionResult LetterBody(int certificateId, int templateId)
        {
            string letter = new Helpers.LetterTemplateHelper().Parse(certificateId, templateId);
            ViewBag.Letter = letter;
            return PartialView("LetterBodyPartial");
        }

        public ActionResult LetterPreview(int certificateId)
        {
            List<BLL.LetterTemplate> templates = repositories.LetterTemplate.GetAll();
            ViewBag.Templates = new SelectList(templates.OrderBy(p => p.Name), "LetterTemplateID", "Name");
            Models.PrintCertificateModel model = new Models.PrintCertificateModel();
            model.SelectedCertificateId = certificateId;
            return View("LetterPreview", model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}