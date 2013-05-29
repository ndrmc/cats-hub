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
    public partial class SessionAttemptRepository :GenericRepository<CTSContext,SessionAttempt>, ISessionAttemptRepository
    {
        public SessionAttemptRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.SessionAttempts.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public SessionAttempt FindById(int id)
        {
            return null;
        }

        public SessionAttempt FindById(System.Guid id)
        {
            return db.SessionAttempts.FirstOrDefault(t => t.SessionAttemptID == id);
        }
    }
}
