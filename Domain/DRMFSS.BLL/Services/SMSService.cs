using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class SMSService:ISMSService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SMSService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool DeleteByID(int id)
        {
            var sms = _unitOfWork.SMSRepository.FindBy(s=>s.SMSID ==id).SingleOrDefault();
            if (sms == null) return false;
            _unitOfWork.SMSRepository.Delete(sms);
            _unitOfWork.Save();
            return true;
        }

        public SMS FindById(int id)
        {
          return  _unitOfWork.SMSRepository.FindBy(s=>s.SMSID ==id).SingleOrDefault();
           
        }
    }
}
