using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{
    partial class Receive
    {
        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            //TODO: Validate Dispatch
            return true;
        }

        /// <summary>
        /// Gets the receive by GRN.
        /// </summary>
        /// <param name="grnNo">The GRN no.</param>
        /// <returns></returns>
        public static Receive GetReceiveByGRN(string grnNo)
        {
            CTSContext entities = new CTSContext();
            return entities.Receives.FirstOrDefault(p => p.GRN == grnNo);
        }

        /// <summary>
        /// Updates the specified inserted.
        /// </summary>
        /// <param name="inserted">The inserted.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="deleted">The deleted.</param>
        public void Update(List<BLL.ReceiveDetail> inserted, List<BLL.ReceiveDetail> updated,
            List<BLL.ReceiveDetail> deleted)
        {
            CTSContext db = new CTSContext();
            BLL.Receive orginal = db.Receives.SingleOrDefault(p => p.ReceiveID == this.ReceiveID);
            if (orginal != null)
            {

                // CreatedDate = DateTime.Now,
                orginal.ReceiptDate = this.ReceiptDate;
                // orginal.ReceiveID = this.ReceiveID;
                orginal.DriverName = this.DriverName;
                orginal.GRN = this.GRN;
                orginal.PlateNo_Prime = this.PlateNo_Prime;
                orginal.PlateNo_Trailer = this.PlateNo_Trailer;
                //orginal.StackNumber = this.StackNumber;
                orginal.TransporterID = this.TransporterID;
                orginal.HubID = this.HubID;
               // orginal.SINumber = this.SINumber;
                //orginal.CommodityTypeID = this.CommodityTypeID;

                orginal.WayBillNo = this.WayBillNo;
                orginal.ResponsibleDonorID = this.ResponsibleDonorID;
                orginal.SourceDonorID = this.SourceDonorID;

                //orginal.ReceivedByStoreMan = this.ReceivedByStoreMan;
                //orginal.CommoditySourceID = this.CommoditySourceID;
                orginal.WeightBeforeUnloading = this.WeightBeforeUnloading;
                orginal.WeightAfterUnloading = this.WeightAfterUnloading;
                orginal.Remark = this.Remark;
                orginal.VesselName = this.VesselName;
                orginal.PortName = this.PortName;
                orginal.WeightBridgeTicketNumber = this.WeightBridgeTicketNumber;

                
                foreach (BLL.ReceiveDetail insert in inserted)
                {
                    //TODO THIS should be in transaction 
                    //orginal.ReceiveDetails.Add(insert);
                }

                foreach (BLL.ReceiveDetail delete in deleted)
                {
                    BLL.ReceiveDetail deletedCommodity = db.ReceiveDetails.SingleOrDefault(p => p.ReceiveDetailID == delete.ReceiveDetailID);
                    if (deletedCommodity != null)
                    {
                  //      db.ReceiveDetails.DeleteObject(deletedCommodity);
                    }
                }

                foreach (BLL.ReceiveDetail update in updated)
                {
                    BLL.ReceiveDetail updatedCommodity = db.ReceiveDetails.SingleOrDefault(p => p.ReceiveDetailID == update.ReceiveDetailID);
                    if (updatedCommodity != null)
                    {
                        //updatedCommodity.CommodityID = update.CommodityID;
                        updatedCommodity.Description = update.Description;
                        //updatedCommodity.ReceiveDetailID = update.ReceiveDetailID;
                        //updatedCommodity.SentQuantityInMT = update.SentQuantityInMT;
                        //updatedCommodity.ReceivedQuantityInMT = updatedCommodity.ReceivedQuantityInMT;
                        //updatedCommodity.SentQuantityInUnit = update.SentQuantityInUnit;
                        //updatedCommodity.UnitID = update.UnitID;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}