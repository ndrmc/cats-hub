using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ITranslationService
    {
        string GetForText(string text, string langauge);
        List<Translation> GetAll(string languageCode);
        bool DeleteByID(int id);
        Translation FindById(int id);

    }
}
