using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class StackEventTypeService:IStackEventTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StackEventTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId)
        {
            var followupDuration = _unitOfWork.StackEventTypeRepository.FindBy(s => s.StackEventTypeID == stackEventTypeId).Select(r => r.DefaultFollowUpDuration.Value).FirstOrDefault();
            return Convert.ToDouble(followupDuration);
        }

        public bool DeleteByID(int id)
        {
            var stackType = _unitOfWork.StackEventTypeRepository.FindBy(s => s.StackEventTypeID == id).SingleOrDefault();
            if (stackType == null) return false;
            _unitOfWork.StackEventTypeRepository.Delete(stackType);
            _unitOfWork.Save();
            return true;
        }

        public StackEventType FindById(int id)
        {
            return _unitOfWork.StackEventTypeRepository.FindBy(s => s.StackEventTypeID == id).SingleOrDefault();
        }
    }
}
