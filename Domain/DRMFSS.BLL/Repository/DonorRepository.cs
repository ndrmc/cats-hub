using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DonorRepository :GenericRepository<CTSContext,Donor>, IDonorRepository
    {
        public DonorRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.DonorViewModel> GetAllSourceDonorForReport()
        {
            var donors = (from c in db.Donors where c.IsSourceDonor == true select new ViewModels.Report.DonorViewModel { DonorId = c.DonorID, DonorName = c.Name }).ToList();
            donors.Insert(0, new ViewModels.Report.DonorViewModel { DonorName = "All Source Donors" });
            return donors;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.Report.DonorViewModel> GetAllResponsibleDonorForReport()
        {
            var donors = (from c in db.Donors where c.IsResponsibleDonor == true select new ViewModels.Report.DonorViewModel { DonorId = c.DonorID, DonorName = c.Name }).ToList();
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
            if(DonorID == null)
            {
                return !(from v in db.Donors
                         where v.DonorCode == DonorCode
                         select v).Any();
            }
            return !(from v in db.Donors
                    where v.DonorCode == DonorCode && v.DonorID != DonorID
                    select v).Any();
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.Donors.Remove(original);
            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Donor FindById(int id)
        {
            return db.Donors.SingleOrDefault(t => t.DonorID == id);
        }

        public Donor FindById(System.Guid id)
        {
            return null;
        }
    }
}
