

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;



namespace DRMFSS.BLL.Services
{

    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DonorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Add(donor);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Edit(donor);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDonor(Donor donor)
        {
            if (donor == null) return false;
            _unitOfWork.DonorRepository.Delete(donor);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DonorRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DonorRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Donor> GetAllDonor()
        {
            return _unitOfWork.DonorRepository.GetAll();
        }
        public Donor FindById(int id)
        {
            return _unitOfWork.DonorRepository.FindById(id);
        }
        public List<Donor> FindBy(Expression<Func<Donor, bool>> predicate)
        {
            return _unitOfWork.DonorRepository.FindBy(predicate);
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.DonorViewModel> GetAllSourceDonorForReport()
        {
            var sourceDonors = _unitOfWork.DonorRepository.Get(t => t.IsSourceDonor);
            var donors = (from c in sourceDonors select new ViewModels.Report.DonorViewModel { DonorId = c.DonorID, DonorName = c.Name }).ToList();
            donors.Insert(0, new ViewModels.Report.DonorViewModel { DonorName = "All Source Donors" });
            return donors;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.DonorViewModel> GetAllResponsibleDonorForReport()
        {
            var responsibleDonors = _unitOfWork.DonorRepository.Get(t => t.IsResponsibleDonor);
            var donors = (from c in responsibleDonors select new ViewModels.Report.DonorViewModel { DonorId = c.DonorID, DonorName = c.Name }).ToList();
            donors.Insert(0, new ViewModels.Report.DonorViewModel { DonorName = "All Responsible Donors" });
            return donors;
        }


        /// <summary>
        /// Determines whether [code is valid] for [the specified donor code].
        /// </summary>
        /// <param name="DonorCode">The donor code.</param>
        /// <param name="DonorID">The donor ID.</param>
        /// <returns></returns>
        public bool IsCodeValid(string DonorCode, int? DonorID)
        {
            if (DonorID == null)
            {
                return !(_unitOfWork.DonorRepository.FindBy(t => t.DonorCode == DonorCode).Any());

            }
            return !(_unitOfWork.DonorRepository.FindBy(t => t.DonorCode == DonorCode && t.DonorID != DonorID).Any());
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


