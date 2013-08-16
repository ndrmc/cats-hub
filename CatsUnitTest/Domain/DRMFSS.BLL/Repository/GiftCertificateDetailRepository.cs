using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GiftCertificateDetailRepository :GenericRepository<CTSContext,GiftCertificateDetail>, IGiftCertificateDetailRepository
    {
        public GiftCertificateDetailRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public List<string> GetUncommitedSIs()
        {
            var list = db.GiftCertificateDetails.Where(
                p => !(p.ReceiptAllocations.Any()) 
                    || (p.ReceiptAllocations.Any(x=>x.IsCommited == false)))
                    .Select(p=>p.GiftCertificate.SINumber).ToList();
                    
               return list;     //.Union(db.ReceiptAllocations.Where(p=>p.SINumber))
                    
        }


        public bool IsBillOfLoadingDuplicate(string billOfLoading)
        {
            return db.GiftCertificateDetails.Any(p => p.BillOfLoading == billOfLoading);
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.GiftCertificateDetails.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public GiftCertificateDetail FindById(int id)
        {
            return db.GiftCertificateDetails.FirstOrDefault(t => t.GiftCertificateDetailID == id);

        }

        public GiftCertificateDetail FindById(System.Guid id)
        {

            return null;
        }
    }
}