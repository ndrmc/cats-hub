using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public  class UnitService:IUnitService
    {
       private readonly IUnitOfWork _unitOfWork;

       public UnitService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
        public bool DeleteByID(int unitId)
        {
            var Unit = _unitOfWork.UnitRepository.FindBy(u => u.UnitID == unitId).SingleOrDefault();
            if (Unit == null) return false;
            _unitOfWork.UnitRepository.Delete(Unit);
            _unitOfWork.Save();
            return true;

            }

        public Unit FindById(int unitId)
        {
            return _unitOfWork.UnitRepository.FindBy(u => u.UnitID == unitId).SingleOrDefault();
        }
    }
}
