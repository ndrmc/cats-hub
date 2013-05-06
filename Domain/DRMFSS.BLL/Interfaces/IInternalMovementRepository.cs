using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.ViewModels;

namespace DRMFSS.BLL.Interfaces
{
   public interface  IInternalMovementRepository : IRepository<InternalMovement>
    {
       void AddNewInternalMovement(InternalMovementViewModel viewModel, UserProfile user);
       List<ViewModels.InternalMovementLogViewModel> GetAllInternalMovmentLog();
    }
}
