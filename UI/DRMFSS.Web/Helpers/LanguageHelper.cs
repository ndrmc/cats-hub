using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Helpers
{
    public static class LanguageHelper
    {
        public static string Translate(this HtmlHelper helper, string text)
        {
            text = text.Trim();
            IUnitOfWork repository = new UnitOfWork();
            if (helper.GetCurrentUser() != null)
            {
                var langauge = helper.GetCurrentUser().LanguageCode;
                return repository.Translation.GetForText(text, langauge);
            }
            return repository.Translation.GetForText(text, "en");
        }

        public static string Translate(this string str)
        {
            return str;
        }
    }
}