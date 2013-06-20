using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class UserProfileService:IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool ChangePassword(int profileId, string password)
        {
            var userPrfl = _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == profileId).SingleOrDefault();
            if (userPrfl == null) return false;
            userPrfl.Password = password;
            _unitOfWork.Save();
            return true;
        }

        public UserProfile GetUser(string userName)
        {
            return _unitOfWork.UserProfileRepository.FindBy(u => u.UserName == userName && !u.LockedInInd && u.ActiveInd).SingleOrDefault();
        }

        public bool EditInfo(UserProfile profile)
        {
            var userPrfl = _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == profile.UserProfileID).SingleOrDefault();
            if (userPrfl == null) return false;
            userPrfl.FirstName = profile.FirstName;
            userPrfl.LastName = profile.LastName;
            userPrfl.GrandFatherName = profile.GrandFatherName;
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByID(int id)
        {
             var userPrfl = _unitOfWork.UserProfileRepository.FindById(id);
            if (userPrfl==null) return false;
            _unitOfWork.UserProfileRepository.Delete(userPrfl);
            _unitOfWork.Save();
            return true;
        }

        public UserProfile FindById(int profileId)
        {
            return _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == profileId).SingleOrDefault();
        }
    }
}
