using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        

        public UserProfile FindById(int profileId)
        {
            return _unitOfWork.UserProfileRepository.FindBy(u => u.UserProfileID == profileId).SingleOrDefault();
        }

        public bool AddUserProfile(UserProfile entity)
        {
            _unitOfWork.UserProfileRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditUserProfile(UserProfile entity)
        {
            _unitOfWork.UserProfileRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteUserProfile(UserProfile entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserProfileRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserProfileRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserProfileRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UserProfile> GetAllUserProfile()
        {
            return _unitOfWork.UserProfileRepository.GetAll();
        }
        //public UserProfile FindById(int id)
        //{
        //    return _unitOfWork.UserProfileRepository.FindById(id);
        //}
        public List<UserProfile> FindBy(Expression<Func<UserProfile, bool>> predicate)
        {
            return _unitOfWork.UserProfileRepository.FindBy(predicate);
        }
    }
}




       
   
 
      
