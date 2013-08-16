using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class AuditSevice : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Implementation of IAuditService

        public bool AddAudit(Audit audit)
        {
            _unitOfWork.AuditRepository.Add(audit);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteAudit(Audit audit)
        {
            if (audit == null) return false;
            _unitOfWork.AuditRepository.Delete(audit);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AuditRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AuditRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditAudit(Audit audit)
        {
            _unitOfWork.AuditRepository.Edit(audit);
            _unitOfWork.Save();
            return true;
        }

        public Audit FindById(int id)
        {
            return _unitOfWork.AuditRepository.FindById(id);
        }

        public List<Audit> GetAllAudit()
        {
            return _unitOfWork.AuditRepository.GetAll();
        }

        public List<Audit> FindBy(Expression<Func<Audit, bool>> predicate)
        {
            return _unitOfWork.AuditRepository.FindBy(predicate);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
