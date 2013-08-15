using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LedgerTypeRepository :GenericRepository<CTSContext,LedgerType>, ILedgerTypeRepository
    {
        public LedgerTypeRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.LedgerTypes.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public LedgerType FindById(int id)
        {
            return db.LedgerTypes.FirstOrDefault(t => t.LedgerTypeID == id);

        }

        public LedgerType FindById(System.Guid id)
        {

            return null;
        }
    }
}
