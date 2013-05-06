using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.ViewModels;

namespace DRMFSS.BLL.Repository
{
   partial class AdjustmentRepository : IAdjustmentRepository
    {
        public List<ViewModels.LossAndAdjustmentLogViewModel> GetAllLossAndAdjustmentLog(int hubId)
        {
            List<ViewModels.LossAndAdjustmentLogViewModel> lossAndAdjustmentsViewModel = new List<ViewModels.LossAndAdjustmentLogViewModel>();

            var lossAndAdjustments = (from c in db.Adjustments 
                                      where c.HubID == hubId
                                      select c);

            foreach (var lossAndAdjustment in lossAndAdjustments)
            {
                lossAndAdjustmentsViewModel.AddRange(from transaction in lossAndAdjustment.TransactionGroup.Transactions
                                                     where transaction.TransactionGroupID == lossAndAdjustment.TransactionGroupID
                                                     where transaction.QuantityInMT >= 0
                                                     select new ViewModels.LossAndAdjustmentLogViewModel
                                                                {
                                                                    TransactionId = transaction.TransactionID, Type = lossAndAdjustment.AdjustmentDirection, CommodityName = repository.Commodity.FindById(transaction.CommodityID).Name, ProjectCodeName = transaction.ProjectCode.Value, MemoNumber = lossAndAdjustment.ReferenceNumber, ShippingInstructionName = transaction.ShippingInstruction.Value, Store = string.Format("{0} - {1} ", transaction.Store.Name, transaction.Store.StoreManName), StoreMan = lossAndAdjustment.StoreManName, Reason = lossAndAdjustment.AdjustmentReason.Name, Description = lossAndAdjustment.Remarks, Unit = transaction.Unit.Name, QuantityInMt = transaction.QuantityInMT, QuantityInUnit = transaction.QuantityInUnit, ApprovedBy = lossAndAdjustment.ApprovedBy, Date = lossAndAdjustment.AdjustmentDate
                                                                });
            }

            return lossAndAdjustmentsViewModel;
        }


        public void AddNewLossAndAdjustment(ViewModels.LossesAndAdjustmentsViewModel viewModel, UserProfile user)
        {
            repository.Transaction.SaveLossAdjustmentTransaction(viewModel, user);
        }
    }
}
