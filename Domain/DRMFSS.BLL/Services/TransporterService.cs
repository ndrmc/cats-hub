using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class TransporterService : ITransporterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransporterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public bool IsNameValid(int? TransporterID, string Name)
        {
             
       
           var Trans = _unitOfWork.TransporterRepository.FindBy(t=>t.Name == Name && t.TransporterID!=TransporterID).Any();
           if (Trans == null) return false;
           return true;

       
        }

        public bool DeleteByID(int TransporterId)
        {
            var Trans = _unitOfWork.TransporterRepository.FindBy(t => t.TransporterID == TransporterId).SingleOrDefault();
            if (Trans == null) return false;
            _unitOfWork.TransporterRepository.Delete(Trans);
            _unitOfWork.Save();
            return true;
        }

        public Transporter FindById(int TransporterId)
        {
            return _unitOfWork.TransporterRepository.FindBy(t => t.TransporterID == TransporterId).SingleOrDefault();
        }
    }
       
}
