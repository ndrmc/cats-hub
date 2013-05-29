using System.Collections.Generic;

namespace DRMFSS.BLL.Interfaces
{

    public interface IAdjustmentRepository :
          IGenericRepository<Adjustment>,IRepository<Adjustment>
    {
        List<ViewModels.LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId);
        void AddNewLossAndAdjustment(ViewModels.LossesAndAdjustmentsViewModel viewModel, UserProfile user);

    }
         
      
}