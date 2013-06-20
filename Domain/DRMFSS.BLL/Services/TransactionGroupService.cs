using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class TransactionGroupService:ITransactionGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid GetLastTrasactionGroupId()
        {
            Guid trasactionGroupId = _unitOfWork.TransactionGroupRepository.GetAll().OrderByDescending(t => t.TransactionGroupID).Select(g => g.TransactionGroupID).First();
            return trasactionGroupId;
        }

        public bool DeleteByID(Guid id)
        {
            var transGroup = _unitOfWork.TransactionGroupRepository.FindBy(g => g.TransactionGroupID == id).FirstOrDefault();
            if (transGroup == null) return false;
            _unitOfWork.TransactionGroupRepository.Delete(transGroup);
            _unitOfWork.Save();
            return true;

        }

        public TransactionGroup FindById(Guid id)
        {
            return _unitOfWork.TransactionGroupRepository.FindBy(g => g.TransactionGroupID == id).FirstOrDefault();
        }
    }
}
