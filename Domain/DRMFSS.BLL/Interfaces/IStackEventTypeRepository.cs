using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.ViewModels;

namespace DRMFSS.BLL.Interfaces
{
  public interface IStackEventTypeRepository : IGenericRepository<StackEventType>,IRepository<StackEventType>
  {
       double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId);
  }
}
