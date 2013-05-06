using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    partial class InternalMovementRepository : IInternalMovementRepository
    {
        
        public void AddNewInternalMovement(ViewModels.InternalMovementViewModel viewModel, UserProfile user)
        {
            repository.Transaction.SaveInternalMovementTrasnsaction(viewModel, user);
        }



        public List<ViewModels.InternalMovementLogViewModel> GetAllInternalMovmentLog()
        {
            List<ViewModels.InternalMovementLogViewModel> internalMovmentLogViewModel = new List<ViewModels.InternalMovementLogViewModel>();
            ViewModels.InternalMovementLogViewModel _internalMovment;


            var internalMovments = (from c in db.InternalMovements select c).ToList();

            foreach (var internalMovment in internalMovments)
            {
                foreach (var transaction in internalMovment.TransactionGroup.Transactions)
                {
                    if (transaction.TransactionGroupID == internalMovment.TransactionGroupID)
                    {
                        if (transaction.QuantityInMT >= 0)
                        {
                            
                            _internalMovment = new ViewModels.InternalMovementLogViewModel();
                            _internalMovment.TransactionId = transaction.TransactionID;
                            _internalMovment.FromStore  = (from c in db.Transactions where ((c.TransactionGroupID == internalMovment.TransactionGroupID) && (c.QuantityInMT < 0)) select (c.Store.Name + " - " + c.Store.StoreManName)).FirstOrDefault();
                            _internalMovment.FromStack = (from c in db.Transactions where ((c.TransactionGroupID == internalMovment.TransactionGroupID) && (c.QuantityInMT < 0)) select c.Stack.Value).FirstOrDefault();
                            _internalMovment.SelectedDate = internalMovment.TransferDate;
                            _internalMovment.ToStore = string.Format("{0} - {1} ", transaction.Store.Name, transaction.Store.StoreManName);
                            _internalMovment.ToStack = transaction.Stack.Value;
                            _internalMovment.RefernaceNumber = internalMovment.ReferenceNumber;
                            _internalMovment.CommodityName = repository.Commodity.FindById(transaction.CommodityID).Name;
                            _internalMovment.Program = transaction.Program.Name;
                            _internalMovment.ProjectCodeName = transaction.ProjectCode.Value;
                            _internalMovment.ShippingInstructionNumber = transaction.ShippingInstruction.Value;
                            _internalMovment.Unit = transaction.Unit.Name;
                            _internalMovment.QuantityInUnit = transaction.QuantityInUnit;
                            _internalMovment.QuantityInMt = transaction.QuantityInMT;
                            _internalMovment.Reason = internalMovment.Detail.Description;
                            _internalMovment.Note = internalMovment.Notes;
                            _internalMovment.ApprovedBy = internalMovment.ApprovedBy;
                            _internalMovment.Reason = internalMovment.Detail.Name;
                            internalMovmentLogViewModel.Add(_internalMovment);
                        }
                    }
                }
            }

            return internalMovmentLogViewModel;
        }
    }
}
