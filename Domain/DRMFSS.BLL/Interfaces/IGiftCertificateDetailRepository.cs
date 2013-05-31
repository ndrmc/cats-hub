using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGiftCertificateDetailRepository :IGenericRepository<GiftCertificateDetail>,IRepository<GiftCertificateDetail>
    {
        List<string> GetUncommitedSIs();

        bool IsBillOfLoadingDuplicate(string billOfLoading);
    }
}
