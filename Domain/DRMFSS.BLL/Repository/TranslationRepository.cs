using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    partial class TranslationRepository: ITranslationRepository
    {
        public string GetForText(string text, string langauge)
        {
            var Translation = (from v in db.Translations
                               where v.Phrase.Trim() == text.Trim() && v.LanguageCode == langauge
                               select v.TranslatedText).FirstOrDefault();
            if(Translation == null)
            {
                // try to see if we can find this phrase in english
                Translation translation = new Translation();
                translation.LanguageCode = langauge;
                translation.Phrase = text.Trim();
                translation.TranslatedText = text.Trim();
                this.Add(translation);
                
                Translation english = null;
                if(langauge != "en")
                {
                    english = (from v in db.Translations
                                   where v.LanguageCode == "en" && v.Phrase == text
                                   select v).FirstOrDefault();

                    
                    
                }

                // Have an entry for it in the database for english
                if(english == null)
                {
                    translation = new Translation();
                    translation.LanguageCode = "en";
                    translation.Phrase = translation.TranslatedText = text.Trim();
                    this.Add(translation);
                }
                else
                {
                    return english.TranslatedText;
                }
                return text;
            }
            return Translation;
        }


        public List<Translation> GetAll(string languageCode)
        {
            return (from v in db.Translations
                    where v.LanguageCode == languageCode
                    select v).OrderBy(o=>o.Phrase).ToList();
        }
    }
}
