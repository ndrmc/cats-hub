using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class TranslationService:ITranslationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TranslationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string GetForText(string text, string langauge)
        {
            var Trans = _unitOfWork.TranslationRepository.FindBy(t => t.Phrase.Trim() == text.Trim() && t.LanguageCode == langauge).FirstOrDefault();
            var Translation = Trans.TranslatedText;

            if (Translation == null)
            {
                Translation translation = new Translation();
                translation.LanguageCode = langauge;
                translation.Phrase = text.Trim();
                translation.TranslatedText = text.Trim();
                _unitOfWork.TranslationRepository.Add(translation);
                _unitOfWork.Save();

                Translation english = null;
                if (langauge != "en")
                {
                    english = _unitOfWork.TranslationRepository.FindBy(t => t.Phrase == text && t.LanguageCode == "en").FirstOrDefault();
                }
                if (english == null)
                {
                    translation = new Translation();
                    translation.LanguageCode = "en";
                    translation.Phrase = translation.TranslatedText = text.Trim();
                    _unitOfWork.TranslationRepository.Add(translation);
                    _unitOfWork.Save();
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
            return _unitOfWork.TranslationRepository.FindBy(t => t.LanguageCode == languageCode).OrderBy(o => o.Phrase).ToList();
        }

        public bool DeleteByID(int id)
        {
            var trans = _unitOfWork.TranslationRepository.FindBy(t => t.TranslationID == id).FirstOrDefault();
            if (trans == null) return false;
            _unitOfWork.TranslationRepository.Delete(trans);
            _unitOfWork.Save();
            return true;
        }

        public Translation FindById(int id)
        {
             return   _unitOfWork.TranslationRepository.FindBy(t => t.TranslationID == id).FirstOrDefault();
        }
    }
}
