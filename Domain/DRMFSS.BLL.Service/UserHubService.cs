

using System;
using System.Collections.Generic;
using System.Linq.Expressions;



namespace DRMFSS.BLL.Services
{

    public class UserHubService : IUserHubService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserHubService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddUserHub(UserHub entity)
        {
            _unitOfWork.UserHubRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditUserHub(UserHub entity)
        {
            _unitOfWork.UserHubRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteUserHub(UserHub entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserHubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserHubRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserHubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UserHub> GetAllUserHub()
        {
            return _unitOfWork.UserHubRepository.GetAll();
        }
        public UserHub FindById(int id)
        {
            return _unitOfWork.UserHubRepository.FindById(id);
        }
        public List<UserHub> FindBy(Expression<Func<UserHub, bool>> predicate)
        {
            return _unitOfWork.UserHubRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


