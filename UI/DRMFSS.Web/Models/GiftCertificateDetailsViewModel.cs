using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DRMFSS.BLL;

namespace DRMFSS.Web.Models
{
    public class GiftCertificateDetailsViewModel
    {
        //[Required(ErrorMessage = "Gift Certificate Detail is required")]
        public Int32 GiftCertificateDetailID { get; set; }

        //[Required(ErrorMessage = "Transaction Group is required")]
        public Int32 TransactionGroupID { get; set; }

        [Required(ErrorMessage = "Gift Certificate is required")]
        public Int32 GiftCertificateID { get; set; }

        [Required(ErrorMessage = "Commodity is required")]
        public Int32 CommodityID { get; set; }

        //public Decimal GrossWeightInMT { get; set; }

        [Required(ErrorMessage = "Weight In M T is required")]
        [Range(0.5, 999999.99)]
        public Decimal WeightInMT { get; set; }

        [StringLength(50)]
        public String BillOfLaoading { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public Int32 AccountNumber { get; set; }

        [Required(ErrorMessage = "Estimated Price is required")]
        [Range(0, 999999.99)]
        public Decimal EstimatedPrice { get; set; }

        [Required(ErrorMessage = "Estimated Tax is required")]
        [Range(0, 999999.99)]
        public Decimal EstimatedTax { get; set; }

        [Required(ErrorMessage = "Year Purchased is required")]
        [Range(2000, 3000)]
        public Int32 YearPurchased { get; set; }

        [Required(ErrorMessage = "D Fund Source is required")]
        public Int32 DFundSourceID { get; set; }

        [Required(ErrorMessage = "D Currency is required")]
        public Int32 DCurrencyID { get; set; }

        [Required(ErrorMessage = "D Budget Type is required")]
        public Int32 DBudgetTypeID { get; set; }

        public GiftCertificateDetailsViewModel()
        {
            this.YearPurchased = 2000;
            this.DBudgetTypeID = 9;
            this.DFundSourceID = 5;
            this.DCurrencyID = 1;
            this.BillOfLaoading = "";
        }

        public static List<GiftCertificateDetailsViewModel> GenerateListOfGiftCertificateDetailsViewModel(System.Data.Objects.DataClasses.EntityCollection<GiftCertificateDetail> entityCollection)
        {
            var details = new List<GiftCertificateDetailsViewModel>();
            foreach (var giftDetail in entityCollection)
            {
                details.Add(GenerateGiftCertificateDetailsViewModel(giftDetail));
            }
            return details;
        }

        public static GiftCertificateDetailsViewModel GenerateGiftCertificateDetailsViewModel(BLL.GiftCertificateDetail giftCertificateDetail)
        {
            GiftCertificateDetailsViewModel model = new GiftCertificateDetailsViewModel();
            
            model.CommodityID = giftCertificateDetail.CommodityID;
            model.BillOfLaoading = giftCertificateDetail.BillOfLaoading;
            model.YearPurchased = giftCertificateDetail.YearPurchased;
            model.AccountNumber = giftCertificateDetail.AccountNumber;
            model.WeightInMT = giftCertificateDetail.WeightInMT;
            model.EstimatedPrice = giftCertificateDetail.EstimatedPrice;
            model.EstimatedTax = giftCertificateDetail.EstimatedTax;
            model.DBudgetTypeID = giftCertificateDetail.DBudgetTypeID;
            model.DFundSourceID = giftCertificateDetail.DFundSourceID;
            model.DCurrencyID = giftCertificateDetail.DCurrencyID;

            return model;
        }
    }
}
