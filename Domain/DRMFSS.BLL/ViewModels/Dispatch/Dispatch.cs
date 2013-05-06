using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{

    /// <summary>
    /// This is a partial class to the Dispatch entity
    /// 
    /// </summary>
    //TODO: this has to be moved to an appropriate folder in the solution. 
    // This is not a view model and should not be placed in the view models folder
    partial class Dispatch
    {
        public bool Validate()
        {
            //TODO: Validate Dispatch
            return true;
        }

        /// <summary>
        /// Updates the specified inserted.
        /// </summary>
        /// <param name="inserted">The inserted.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="deleted">The deleted.</param>
        public void Update(List<BLL.DispatchDetail> inserted, List<BLL.DispatchDetail> updated, 
            List<BLL.DispatchDetail> deleted)
        {
            DRMFSSEntities1 db = new DRMFSSEntities1();
            BLL.Dispatch orginal = db.Dispatches.Where(p => p.DispatchID == this.DispatchID).SingleOrDefault();
                    if (orginal != null)
                    {
                        orginal.BidNumber = this.BidNumber;
                        orginal.DispatchDate = this.DispatchDate;
                        orginal.DriverName = this.DriverName;
                        orginal.FDPID = this.FDPID;
                        orginal.GIN = this.GIN;
                        orginal.PeriodYear = this.PeriodYear;
                        orginal.PeriodMonth = this.PeriodMonth;
                        orginal.PlateNo_Prime = this.PlateNo_Prime;
                        orginal.PlateNo_Trailer = this.PlateNo_Trailer;
                        //orginal.ProgramID = this.ProgramID;
                        orginal.RequisitionNo = this.RequisitionNo;
                        orginal.Round = this.Round;
                        //orginal.StackNumber = this.StackNumber;
                        //orginal.StoreID = this.StoreID;
                        orginal.TransporterID = this.TransporterID;
                        orginal.UserProfileID = this.UserProfileID;
                        //orginal.WarehouseID = this.WarehouseID;
                        orginal.WeighBridgeTicketNumber = this.WeighBridgeTicketNumber;
                        orginal.Remark = this.Remark;
                        orginal.DispatchedByStoreMan = this.DispatchedByStoreMan;
                        //orginal.ProjectNumber = this.ProjectNumber;
                        //orginal.SINumber = this.SINumber;


                  
                        

                        foreach (BLL.DispatchDetail update in updated)
                        {
                            BLL.DispatchDetail updatedCommodity = db.DispatchDetails.Where(p => p.DispatchDetailID == update.DispatchDetailID).SingleOrDefault();
                            if (updatedCommodity != null)
                            {
                                updatedCommodity.CommodityID = update.CommodityID;
                                updatedCommodity.Description = update.Description;
                                //updatedCommodity.DispatchedQuantityInUnit = update.DispatchedQuantityInUnit;
                                //updatedCommodity.DispatchedQuantityInMT = update.DispatchedQuantityInMT;
                                updatedCommodity.RequestedQunatityInUnit = update.RequestedQunatityInUnit;
                                updatedCommodity.RequestedQuantityInMT = update.RequestedQuantityInMT;
                                updatedCommodity.UnitID = update.UnitID;
                            }
                        }
                        db.SaveChanges();
                    }
        }

        /// <summary>
        /// Gets the SMS text.
        /// </summary>
        /// <returns></returns>
        public string GetSMSText()
        {
            StringBuilder builder = new StringBuilder();
            DRMFSSEntities1 entities = new DRMFSSEntities1();
            BLL.Dispatch dispatch  = entities.Dispatches.Where(d => d.DispatchID == this.DispatchID).SingleOrDefault();
            if (dispatch != null)
            {
                BLL.DispatchDetail com = dispatch.DispatchDetails.FirstOrDefault();
                if (com != null)
                {
                    builder.Append(string.Format("There is a Dispatch with an ammount of {0}(MT) - {1} to your FDP({2}) ", com.RequestedQuantityInMT, com.Commodity.Name, dispatch.FDP.Name));
                    builder.Append(string.Format("on a car with plate no - {0}", dispatch.PlateNo_Prime));
                }
            }
            
            return builder.ToString();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public int Type
        {
            get
            {
                return (FDPID.HasValue) ? 1 : 2;
            }
        }
    }
}
