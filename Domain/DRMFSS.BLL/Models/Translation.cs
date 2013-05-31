using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Translation
    {
        public int TranslationID { get; set; }
        public string LanguageCode { get; set; }
        public string Phrase { get; set; }
        public string TranslatedText { get; set; }
    }
}
