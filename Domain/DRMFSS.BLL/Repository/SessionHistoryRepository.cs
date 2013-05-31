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
    public partial class SessionHistoryRepository :GenericRepository<CTSContext,SessionHistory> ,ISessionHistoryRepository
    {
        public SessionHistoryRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.SessionHistories.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public SessionHistory FindById(int id)
        {
            return null;
        }

        public SessionHistory FindById(System.Guid id)
        {
            return db.SessionHistories.FirstOrDefault(t => t.SessionHistoryID == id);
        }
    }
}
