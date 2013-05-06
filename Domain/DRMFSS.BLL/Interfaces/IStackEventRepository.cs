using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.ViewModels;

namespace DRMFSS.BLL.Interfaces
{
  public interface IStackEventRepository : IRepository<StackEvent>
    {
      List<StackEventLogViewModel> GetAllStackEvents(UserProfile user);

      List<StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId);
    }
}
