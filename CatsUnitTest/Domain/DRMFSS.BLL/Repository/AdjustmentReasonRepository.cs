using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    partial class AdjustmentReasonRepository : GenericRepository<CTSContext, AdjustmentReason>, IAdjustmentReasonRepository
    {
        public AdjustmentReasonRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.AdjustmentReasons.Remove(original);
            this.db.SaveChanges();
            return true;

        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public AdjustmentReason FindById(Guid id)
        {
            return null;

        }

        public AdjustmentReason FindById(int id)
        {
            return db.AdjustmentReasons.SingleOrDefault(p => p.AdjustmentReasonID == id);


        }
    }
}
