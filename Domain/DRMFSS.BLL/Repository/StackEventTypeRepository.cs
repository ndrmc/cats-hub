using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class StackEventTypeRepository : IStackEventTypeRepository
    {
        public double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId)
        {
            var followupDuration = (from c in db.StackEventTypes where c.StackEventTypeID == stackEventTypeId select c.DefaultFollowUpDuration.Value).FirstOrDefault();
            return Convert.ToDouble(followupDuration);
        }
    }
}
