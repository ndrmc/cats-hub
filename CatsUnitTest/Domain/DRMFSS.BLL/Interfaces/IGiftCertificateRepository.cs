using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    public interface IGiftCertificateRepository : IGenericRepository<GiftCertificate>,IRepository<GiftCertificate>
    {
        /// <summary>
        /// Gets the monthly summary.
        /// </summary>
        /// <returns></returns>
        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummary();
        /// <summary>
        /// Gets the monthly summary ETA.
        /// </summary>
        /// <returns></returns>
        ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummaryETA();
        /// <summary>
        /// Updates the specified gift certificate model.
        /// </summary>
        /// <param name="giftCertificateModel">The gift certificate model.</param>
        /// <param name="generateGiftCertificate">The generate gift certificate.</param>
        /// <param name="giftCertificateDetails">The gift certificate details.</param>
        /// <param name="list">The list.</param>
        void Update(GiftCertificate giftCertificateModel, List<GiftCertificateDetail> generateGiftCertificate, List<GiftCertificateDetail> giftCertificateDetails, List<GiftCertificateDetail> list);
        /// <summary>
        /// Finds the by SI number.
        /// </summary>
        /// <param name="SInumber">The S inumber.</param>
        /// <returns></returns>
        GiftCertificate FindBySINumber(string SInumber);
        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        List<BLL.SIBalance> GetSIBalances();
    }
}
