using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class StackEventRepository : IStackEventRepository
    {
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


    }
}
