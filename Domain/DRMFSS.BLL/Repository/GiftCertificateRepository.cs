using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;


namespace DRMFSS.BLL.Repository
{
    public partial class GiftCertificateRepository :GenericRepository<CTSContext,GiftCertificate>, IGiftCertificateRepository
    {

        public GiftCertificateRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// Gets the monthly summary.
        /// </summary>
        /// <returns></returns>
        public ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummary()
        {
            return db.GetMonthlyGiftSummary();
        }


        /// <summary>
        /// Updates the specified gift certificate model.
        /// </summary>
        /// <param name="giftCertificateModel">The gift certificate model.</param>
        /// <param name="inserted">The inserted gift certificate detail.</param>
        /// <param name="updated">The updated gift certificate detail.</param>
        /// <param name="deleted">The deleted gift certificate detail.</param>
        public void Update(GiftCertificate giftCertificateModel, List<GiftCertificateDetail> inserted,
            List<GiftCertificateDetail> updated, List<GiftCertificateDetail> deleted)
        {

           // DRMFSSEntities1 db = new DRMFSSEntities1();
            BLL.GiftCertificate orginal = db.GiftCertificates.SingleOrDefault(p => p.GiftCertificateID == giftCertificateModel.GiftCertificateID);
            if (orginal != null)
            {

                orginal.GiftDate = giftCertificateModel.GiftDate;
                orginal.DonorID = giftCertificateModel.DonorID;
                orginal.SINumber = giftCertificateModel.SINumber;
                orginal.ReferenceNo = giftCertificateModel.ReferenceNo;
                orginal.Vessel = giftCertificateModel.Vessel;
                orginal.ETA = giftCertificateModel.ETA;
                orginal.ProgramID = giftCertificateModel.ProgramID;
                orginal.PortName = giftCertificateModel.PortName;
                orginal.DModeOfTransport = giftCertificateModel.DModeOfTransport;
                
                foreach (BLL.GiftCertificateDetail insert in inserted)
                {
                    orginal.GiftCertificateDetails.Add(insert);
                }

                foreach (BLL.GiftCertificateDetail delete in deleted)
                {
                    BLL.GiftCertificateDetail deletedGiftDetails = db.GiftCertificateDetails.SingleOrDefault(p => p.GiftCertificateDetailID == delete.GiftCertificateDetailID);
                    if (deletedGiftDetails != null)
                    {
                        db.GiftCertificateDetails.Remove(deletedGiftDetails);
                    }
                }

                foreach (BLL.GiftCertificateDetail update in updated)
                {
                    BLL.GiftCertificateDetail updatedGiftDetails = db.GiftCertificateDetails.SingleOrDefault(p => p.GiftCertificateDetailID == update.GiftCertificateDetailID);
                    if (updatedGiftDetails != null)
                    {
                        updatedGiftDetails.CommodityID = update.CommodityID;
                        updatedGiftDetails.BillOfLoading = update.BillOfLoading;
                        updatedGiftDetails.YearPurchased = update.YearPurchased;
                        updatedGiftDetails.AccountNumber = update.AccountNumber;
                        updatedGiftDetails.WeightInMT = update.WeightInMT;
                        updatedGiftDetails.EstimatedPrice = update.EstimatedPrice;
                        updatedGiftDetails.EstimatedTax = update.EstimatedTax;
                        updatedGiftDetails.DCurrencyID = update.DCurrencyID;
                        updatedGiftDetails.DFundSourceID = update.DFundSourceID;
                        updatedGiftDetails.DBudgetTypeID = update.DBudgetTypeID;
                        updatedGiftDetails.ExpiryDate = update.ExpiryDate;
                    }
                }
                db.SaveChanges();
            }

        }


        /// <summary>
        /// Gets the monthly summary ETA.
        /// </summary>
        /// <returns></returns>
        public ObjectResult<RPT_MonthlyGiftSummary_Result> GetMonthlySummaryETA()
        {
            return db.GetMonthlyGiftSummaryETA();
        }


        /// <summary>
        /// Finds the by SI number.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public GiftCertificate FindBySINumber(string SINumber)
        {
            return db.GiftCertificates.ToList().FirstOrDefault(p => p.SINumber == SINumber);
        }


        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        public List<SIBalance> GetSIBalances()
        {
            var list = (from GCD in db.GiftCertificateDetails
                        group GCD by GCD.GiftCertificate.SINumber into si
                        select new SIBalance() { SINumber = si.Key, AvailableBalance = si.Sum(p => p.WeightInMT) }).ToList();

            return list;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.GiftCertificates.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public GiftCertificate FindById(int id)
        {

            return db.GiftCertificates.FirstOrDefault(t => t.GiftCertificateID == id);
        }

        public GiftCertificate FindById(System.Guid id)
        {

            return null;
        }
    }
}
