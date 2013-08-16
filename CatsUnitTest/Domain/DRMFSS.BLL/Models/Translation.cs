using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRMFSS.BLL
{
    public partial class Translation
    {
        [Key]
        public int TranslationID { get; set; }
        public string LanguageCode { get; set; }
        public string Phrase { get; set; }
        public string TranslatedText { get; set; }
    }
}
