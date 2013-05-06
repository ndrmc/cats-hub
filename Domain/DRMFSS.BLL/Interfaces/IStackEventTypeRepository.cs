using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Interfaces
{
    public interface IStackEventTypeRepository : IRepository<StackEventType>
    {
        double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId);
    }
}
