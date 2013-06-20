using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class UserHubService:IUserHubService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHubService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool DeleteByID(int HubId)
        {
            var Hub = _unitOfWork.UserHubRepository.FindBy(h => h.HubID == HubId).SingleOrDefault();
            if (Hub == null) return false;
            _unitOfWork.UserHubRepository.Delete(Hub);
            _unitOfWork.Save();
            return true;
        }

        public UserHub FindById(int HubId)
        {
            return _unitOfWork.UserHubRepository.FindBy(h => h.HubID == HubId).SingleOrDefault();
        }
    }
}
