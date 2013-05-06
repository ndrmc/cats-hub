using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using DRMFSS.BLL;

namespace DRMFSS.Web.Models
{
    public class GiftCertificateViewModel
    {
        [Required(ErrorMessage = "Gift Certificate is required")]
        public Int32 GiftCertificateID { get; set; }

        [Required(ErrorMessage = "Gift Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime GiftDate { get; set; }

        [Required(ErrorMessage = "Donor is required")]
        public Int32 DonorID { get; set; }

        [Required(ErrorMessage = "SI Number is required")]
        [StringLength(50)]
        public String SINumber { get; set; }

        [Required(ErrorMessage = "Reference No is required")]
        [StringLength(50)]
        public String ReferenceNo { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vessel is required")]
        public String Vessel { get; set; }

        [Required(ErrorMessage = "ETA is required")]
        [DataType(DataType.DateTime)]
        public DateTime ETA { get; set; }

        [Required(ErrorMessage = "Is Printed is required")]
        public Boolean IsPrinted { get; set; }

        [Required(ErrorMessage = "Program is required")]
        public Int32 ProgramID { get; set; }

        [Required(ErrorMessage = "Mode Of Transport is required")]
        public Int32 DModeOfTransport { get; set; }

        [StringLength(50)]
        public String PortName { get; set; }


        //public EntityCollection<Donor> Donor { get; set; }

        public List<GiftCertificateDetailsViewModel> GiftCertificateDetails { get; set; }

        public string JSONInsertedGiftCertificateDetails { get; set; }

        public string JSONDeletedGiftCertificateDetails { get; set; }

        public string JSONUpdatedGiftCertificateDetails { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "Please insert at least one commodity")]
        public int rowCount { get; set; }

        public static Models.GiftCertificateViewModel GiftCertificateModel(BLL.GiftCertificate GiftCertificateModel)
        {
            GiftCertificateViewModel giftCertificateViewModel = new GiftCertificateViewModel();

            giftCertificateViewModel.GiftCertificateID = GiftCertificateModel.GiftCertificateID;
            giftCertificateViewModel.GiftDate = GiftCertificateModel.GiftDate;
            giftCertificateViewModel.DonorID = GiftCertificateModel.DonorID;
            giftCertificateViewModel.SINumber = GiftCertificateModel.SINumber;
            giftCertificateViewModel.ReferenceNo = GiftCertificateModel.ReferenceNo;
            giftCertificateViewModel.Vessel = GiftCertificateModel.Vessel;
            giftCertificateViewModel.ETA = GiftCertificateModel.ETA;
            giftCertificateViewModel.ProgramID = GiftCertificateModel.ProgramID;
            giftCertificateViewModel.PortName = GiftCertificateModel.PortName;
            giftCertificateViewModel.DModeOfTransport = GiftCertificateModel.DModeOfTransport;
            giftCertificateViewModel.GiftCertificateDetails =
                GiftCertificateDetailsViewModel.GenerateListOfGiftCertificateDetailsViewModel(
                    GiftCertificateModel.GiftCertificateDetails);

            return giftCertificateViewModel;
        }


        public BLL.GiftCertificate GenerateGiftCertificate()
        {
            GiftCertificate giftCertificate = new GiftCertificate()
                                                  {
                                                      GiftCertificateID = this.GiftCertificateID,
                                                      GiftDate = this.GiftDate,
                                                      SINumber = this.SINumber,
                                                      DonorID = this.DonorID,
                                                      ReferenceNo = this.ReferenceNo,
                                                      Vessel = this.Vessel,
                                                      ETA = this.ETA,
                                                      IsPrinted = this.IsPrinted,
                                                      DModeOfTransport  = this.DModeOfTransport,
                                                      ProgramID = this.ProgramID,
                                                      PortName = this.PortName
                                                  };
            return giftCertificate;
        }

     }
}