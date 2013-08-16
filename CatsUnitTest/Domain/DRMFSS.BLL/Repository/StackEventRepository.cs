using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class StackEventRepository :GenericRepository<CTSContext,StackEvent>, IStackEventRepository
    {
        public StackEventRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public List<ViewModels.StackEventLogViewModel> GetAllStackEvents(UserProfile user)
        {
            var events = (from c in db.StackEvents select new ViewModels.StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
            return events;
        }


        public List<ViewModels.StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId)
        {
            var events = (from c in db.StackEvents where (c.StackNumber == StackId && c.StoreID == StoreId ) select new ViewModels.StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
            return events;
        }



        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.StackEvents.Remove(original);
            this.db.SaveChanges();
            return true;

        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public StackEvent FindById(Guid id)
        {
            return db.StackEvents.SingleOrDefault(p => p.StackEventID == id);

        }

        public StackEvent FindById(int id)
        {
            return null;


        }
    }
}
