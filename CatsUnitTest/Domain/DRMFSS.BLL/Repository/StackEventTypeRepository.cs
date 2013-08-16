using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class StackEventTypeRepository :GenericRepository<CTSContext,StackEventType>, IStackEventTypeRepository
    {
        public StackEventTypeRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId)
        {
            var followupDuration = (from c in db.StackEventTypes where c.StackEventTypeID == stackEventTypeId select c.DefaultFollowUpDuration.Value).FirstOrDefault();
            return Convert.ToDouble(followupDuration);
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.StackEventTypes.Remove(original);
            this.db.SaveChanges();
            return true;

        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public StackEventType FindById(Guid id)
        {
            return null;

        }

        public StackEventType FindById(int id)
        {
            return db.StackEventTypes.SingleOrDefault(p => p.StackEventTypeID == id);


        }
    }
}
