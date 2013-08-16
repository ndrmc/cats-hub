
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IAdjustmentReasonService
    {

        bool AddAdjustmentReason(AdjustmentReason adjustmentReason);
        bool DeleteAdjustmentReason(AdjustmentReason adjustmentReason);
        bool DeleteById(int id);
        bool EditAdjustmentReason(AdjustmentReason adjustmentReason);
        AdjustmentReason FindById(int id);
        List<AdjustmentReason> GetAllAdjustmentReason();
        List<AdjustmentReason> FindBy(Expression<Func<AdjustmentReason, bool>> predicate);


    }
}


