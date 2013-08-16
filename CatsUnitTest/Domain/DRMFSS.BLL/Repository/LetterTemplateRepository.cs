using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LetterTemplateRepository :GenericRepository<CTSContext,LetterTemplate>, ILetterTemplateRepository
    {
        public LetterTemplateRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }


        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.LetterTemplates.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public LetterTemplate FindById(int id)
        {
            return db.LetterTemplates.FirstOrDefault(t => t.LetterTemplateID == id);
        }

        public LetterTemplate FindById(System.Guid id)
        {
            return null;
        }
    }
}
