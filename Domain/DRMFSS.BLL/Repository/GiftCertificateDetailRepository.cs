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
    public partial class GiftCertificateDetailRepository : IGiftCertificateDetailRepository
    {


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
    }
}