using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUnitOfWork _unitOfWork;


        public UserRoleService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public bool DeleteByID(int id)
        {
            var entity = _unitOfWork.UserRoleRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserRoleRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
           
        }


        public UserRole FindById(int id)
        {
            return _unitOfWork.UserRoleRepository.FindById(id);
        }
        

       
    }
}
