using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Interfaces
{
    public interface ITranslationRepository : IGenericRepository<Translation>,IRepository<Translation>
    {
        string GetForText(string text, string langauge);

        List<Translation> GetAll(string p);
    }
}
