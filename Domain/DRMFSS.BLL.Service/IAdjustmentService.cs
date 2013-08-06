
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IAdjustmentService
    {

        bool AddAdjustment(Adjustment adjustment);
        bool DeleteAdjustment(Adjustment adjustment);
        bool DeleteById(int id);
        bool EditAdjustment(Adjustment adjustment);
        Adjustment FindById(int id);
        List<Adjustment> GetAllAdjustment();
        List<Adjustment> FindBy(Expression<Func<Adjustment, bool>> predicate);
        List<ViewModels.LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId);
        void AddNewLossAndAdjustment(ViewModels.LossesAndAdjustmentsViewModel viewModel, UserProfile user);
    }
}


