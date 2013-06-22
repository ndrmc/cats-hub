

using System;
using System.Collections.Generic;
using System.Linq.Expressions;



namespace DRMFSS.BLL.Services
{

    public class AdminUnitService : IAdminUnitService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminUnitService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddAdminUnit(AdminUnit entity)
        {
            _unitOfWork.AdminUnitRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAdminUnit(AdminUnit entity)
        {
            _unitOfWork.AdminUnitRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAdminUnit(AdminUnit entity)
        {
            if (entity == null) return false;
            _unitOfWork.AdminUnitRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AdminUnitRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AdminUnitRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<AdminUnit> GetAllAdminUnit()
        {
            return _unitOfWork.AdminUnitRepository.GetAll();
        }
        public AdminUnit FindById(int id)
        {
            return _unitOfWork.AdminUnitRepository.FindById(id);
        }
        public List<AdminUnit> FindBy(Expression<Func<AdminUnit, bool>> predicate)
        {
            return _unitOfWork.AdminUnitRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


