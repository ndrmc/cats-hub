using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IStackEventTypeService
    {
        double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId);
        bool DeleteByID(int id);
        StackEventType FindById(int id);

    }
}
