using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ITranslationService
    {
        bool AddTranslation(Translation entity);
        bool DeleteTranslation(Translation entity);
        bool DeleteById(int id);
        bool EditTranslation(Translation entity);
        Translation FindById(int id);
        List<Translation> GetAllTranslation();
        List<Translation> FindBy(Expression<Func<Translation, bool>> predicate);

        string GetForText(string text, string langauge);
        List<Translation> GetAll(string languageCode);
        

    }
}

      

      
