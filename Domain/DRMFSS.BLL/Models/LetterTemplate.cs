using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class LetterTemplate
    {
        public int LetterTemplateID { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public string Template { get; set; }
    }
}
