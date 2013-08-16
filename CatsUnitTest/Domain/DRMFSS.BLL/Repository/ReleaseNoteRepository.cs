using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReleaseNoteRepository :GenericRepository<CTSContext,ReleaseNote>, IReleaseNoteRepository
    {
        public ReleaseNoteRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.ReleaseNotes.Remove(original);
                this.db.SaveChanges();
                return true;
           
        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public ReleaseNote FindById(Guid id)
        {
            return null;
        }

        public ReleaseNote FindById(int id)
        {
            return db.ReleaseNotes.SingleOrDefault(p => p.ReleaseNoteID == id);

        }
    }
}
