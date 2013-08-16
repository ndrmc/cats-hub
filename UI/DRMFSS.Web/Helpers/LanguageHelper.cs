using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Helpers
{
    public static class LanguageHelper
    {
        public static string Translate(this HtmlHelper helper, string text)
        {
            text = text.Trim();
           // IUnitOfWork repository = new UnitOfWork();
            UnitOfWork unitOfWork = new UnitOfWork();
            TranslationService translationService = new TranslationService(unitOfWork);
            if (helper.GetCurrentUser() != null)
            {
                var langauge = helper.GetCurrentUser().LanguageCode;
                return translationService.GetForText(text, langauge);
            }
            return translationService.GetForText(text, "en");
        }

        public static string Translate(this string str)
        {
            return str;
        }
    }
}