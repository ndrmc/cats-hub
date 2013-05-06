using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.ViewModels;

namespace DRMFSS.BLL.Interfaces
{
   public  interface IAdjustmentRepository : IRepository<Adjustment>
    {
       List<LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId);

       void AddNewLossAndAdjustment(LossesAndAdjustmentsViewModel viewModel, UserProfile user);
       
    }
}
