
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IGiftCertificateDetailService
    {

        bool AddGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail);
        bool DeleteGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail);
        bool DeleteById(int id);
        bool EditGiftCertificateDetail(GiftCertificateDetail giftCertificateDetail);
        GiftCertificateDetail FindById(int id);
        List<GiftCertificateDetail> GetAllGiftCertificateDetail();
        List<GiftCertificateDetail> FindBy(Expression<Func<GiftCertificateDetail, bool>> predicate);

        List<string> GetUncommitedSIs();

        bool IsBillOfLoadingDuplicate(string billOfLoading);
    }
}


