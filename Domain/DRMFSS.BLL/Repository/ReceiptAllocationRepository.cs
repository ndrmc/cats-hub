using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
using System.Data.Objects.DataClasses;
using DRMFSS.Web.Models;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReceiptAllocationRepository : IReceiptAllocationRepository
    {
        /// <summary>
        /// Finds the by SI number.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public List<ReceiptAllocation> FindBySINumber(string SINumber)
        {
            return db.ReceiptAllocations.Where(p => p.SINumber == SINumber).ToList();
        }

        /// <summary>
        /// Gets the uncommited allocation transaction.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="ShipingInstructionID">The shiping instruction ID.</param>
        /// <param name="HubID">The hub ID.</param>
        /// <returns></returns>
        public Transaction GetUncommitedAllocationTransaction(int CommodityID, int ShipingInstructionID, int HubID)
        {
            return (from tr in db.Transactions
                    where tr.CommodityID == CommodityID && tr.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                    && tr.ShippingInstructionID == ShipingInstructionID && tr.HubID == HubID
                    select tr).FirstOrDefault();
        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="siNumber">The si number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        public decimal GetBalance(string siNumber, int commodityId)
        {
            decimal total = (from ra in db.ReceiptAllocations
                             where ra.SINumber == siNumber && ra.CommodityID == commodityId
                             select ra.QuantityInMT).Sum();
            return total;
        }

        /// <summary>
        /// Gets the balance for SI.
        /// </summary>
        /// <param name="SInumber">The S inumber.</param>
        /// <param name="hubID"> </param>
        /// <returns></returns>
        public decimal GetBalanceForSI(string SInumber)
        {
            decimal total = 0;

            var data = (from ra in db.ReceiptAllocations
                        where ra.SINumber == SInumber && ra.QuantityInMT != null 
                        select ra.QuantityInMT);

            if(data != null && data.Count() > 0)
            {
                total = data.Sum();
            }

            return total;
        }

        public List<Commodity> GetAvailableCommodities(string SINumber,int hubId)
        {
            var query = (from q in db.ReceiptAllocations
                         where q.SINumber == SINumber && q.HubID == hubId
                         select q.Commodity).Distinct();//.OrderBy(p=>p.ParentID).GroupBy(p=>p.ParentID);

            List<Commodity> optGroupList = new List<Commodity>();

            foreach (var commodity in query)
            {
                if(commodity.ParentID == null)//parent
                {
                    if(!(optGroupList.Exists(p=>p.CommodityID == commodity.CommodityID)))
                    {
                        optGroupList.Add(commodity);
                        foreach (var childComm in commodity.Commodity1)
                        {
                            if (!query.Any(p => p.CommodityID == childComm.CommodityID))
                                optGroupList.Add(childComm);
                        }
                    }
                }
                else //child
                {
                    if (!(optGroupList.Exists(p => p.CommodityID == commodity.CommodityID)))
                    {
                        if (!(optGroupList.Exists(p => p.CommodityID == commodity.Commodity2.CommodityID)))
                        {
                            optGroupList.Add(commodity.Commodity2);
                        }
                        optGroupList.Insert(optGroupList.IndexOf(commodity.Commodity2)+1,commodity);
                    }
                }
            }

            //var query1 = (from q in db.ReceiptAllocations
            //              where q.SINumber == SINumber && q.Commodity.ParentID != null
            //              select q.Commodity.Commodity2).Distinct();

            //IQueryable<Commodity> commodities = (from q in query
            //                          join q1 in query1 on q.CommodityID equals q1.CommodityID
            //                          where true
            //                          select q).Distinct();


            //List<Commodity> parents = new List<Commodity>();
            //foreach (Commodity commodity in query)
            //{
            //    if (commodity.ParentID == null)
            //    {
            //        parents.Add(commodity);
            //    }
            //    else
            //    {
            //        parents.Add(commodity);
            //    }
            //}
            return optGroupList;
        }

        public List<string> GetSIsWithOutGiftCertificate()
        {
            var query = (from q in db.ReceiptAllocations
                         where q.GiftCertificateDetail == null
                         select q.SINumber).Distinct();
            return query.ToList();          
        }

        public decimal GetTotalAllocation(string siNumber, int commodityId, int hubId, int? commoditySourceId)
        {
            decimal totalAllocation = 0;
            //Commodity commodity = repository.Commodity.FindById(commodityId);
            //if (commodity.ParentID != null)
            //{
            //    commodityId = commodity.ParentID.Value;
            //}
            //var x = GetListOfSource(commoditySourceId.Value);
            var allocationSum = (from v in db.ReceiptAllocations
                                 where v.SINumber == siNumber
                                       && v.HubID == hubId
                                       && v.IsClosed == false
                                       && v.CommodityID == commodityId
                                       && v.CommoditySourceID == commoditySourceId
                                       && v.QuantityInMT > 0
                                 select v.QuantityInMT);

            if (allocationSum.Count() > 0)
            {
                totalAllocation = allocationSum.Sum();
            }
            return totalAllocation;
        }

        /// <summary>
        /// Commits the receive allocation.
        /// </summary>
        /// <param name="checkedRecords">The checked records.</param>
        /// <param name="user"></param>
        public void CommitReceiveAllocation(string[] checkedRecords, UserProfile user)
        {
                foreach (string id in checkedRecords)
                {
                        db.ReceiptAllocations.MergeOption = MergeOption.PreserveChanges;
                        ReceiptAllocation original = db.ReceiptAllocations.FirstOrDefault(p => p.ReceiptAllocationID == Guid.Parse(id));
                        if (original != null)
                        {
                            original.IsCommited = true;
                            db.SaveChanges();
                            db.ReceiptAllocations.MergeOption = MergeOption.NoTracking;
                        }
                        db.ReceiptAllocations.MergeOption = MergeOption.NoTracking;
                }
           
        }

        public int[] GetListOfSource(int commoditySoureType)
        {
            var x = new int[] { };

            if (commoditySoureType == BLL.CommoditySource.Constants.DONATION)
            //TODO remove this Condition line later
            {
                x = new int[] { BLL.CommoditySource.Constants.DONATION };
            }
            else if (commoditySoureType == BLL.CommoditySource.Constants.LOCALPURCHASE)
            {
                x = new int[] { BLL.CommoditySource.Constants.LOCALPURCHASE };
            }
            //else if (commoditySoureType == BLL.CommoditySource.Constants.TRANSFER)
            //{
            //    x = new int[] { BLL.CommoditySource.Constants.TRANSFER };
            //}
            else if (BLL.CommoditySource.Constants.TRANSFER == commoditySoureType  ||
                     BLL.CommoditySource.Constants.REPAYMENT == commoditySoureType ||
                     BLL.CommoditySource.Constants.LOAN == commoditySoureType ||
                     BLL.CommoditySource.Constants.SWAP == commoditySoureType)
            {
                x = new int[]
                            {
                                BLL.CommoditySource.Constants.REPAYMENT,
                                BLL.CommoditySource.Constants.LOAN,
                                BLL.CommoditySource.Constants.SWAP,
                                BLL.CommoditySource.Constants.TRANSFER
                            };
            }
            return x;
        }

        public List<ReceiptAllocation> GetUnclosedAllocationsDetached(int hubId, int commoditySoureType, bool? closedToo, string weightMeasurmentCode, int? CommodityType )
        {
            List<ReceiptAllocation> GetDetachecedList = new List<ReceiptAllocation>();

            var x = GetListOfSource(commoditySoureType);

            var unclosed = (from rAll in db.ReceiptAllocations
                            where hubId == rAll.HubID
                                  && x.Any(p=>p == rAll.CommoditySourceID)
                            select rAll);

            if (closedToo == null || closedToo == false)
            {
                unclosed = unclosed.Where(p => p.IsClosed == false);
            }else
            {
                unclosed = unclosed.Where(p => p.IsClosed == true);
            }


            if (CommodityType.HasValue)
            {
                unclosed = unclosed.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value);
            }
            else
            {
                unclosed = unclosed.Where(p => p.Commodity.CommodityTypeID == 1);//by default
            }

            foreach (ReceiptAllocation receiptAllocation in unclosed)
            {
                
                int si = repository.ShippingInstruction.GetShipingInstructionId(receiptAllocation.SINumber);
                receiptAllocation.RemainingBalanceInMt = receiptAllocation.QuantityInMT -
                                                          GetReceivedAlready(
                                                         receiptAllocation);
                receiptAllocation.CommodityName = receiptAllocation.Commodity.Name;

         
                if (receiptAllocation.QuantityInUnit != null)
                    receiptAllocation.RemainingBalanceInUnit = receiptAllocation.QuantityInUnit.Value -
                                                               GetReceivedAlreadyInUnit(receiptAllocation);
                else
                    receiptAllocation.RemainingBalanceInUnit = 0 -
                                           GetReceivedAlreadyInUnit(receiptAllocation);


                if (weightMeasurmentCode == "qn")
                {
                    receiptAllocation.QuantityInMT *= 10;
                    receiptAllocation.RemainingBalanceInMt *= 10;
                    receiptAllocation.ReceivedQuantityInMT *= 10;
                }

               db.Detach(receiptAllocation);
               GetDetachecedList.Add(receiptAllocation);
            }

            return GetDetachecedList.ToList();

        }

        public decimal GetReceivedAlready(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + Math.Abs(rd.QuantityInMT);
                    }
                }
            return sum;
        }

        public decimal GetReceivedAlreadyInUnit(ReceiptAllocation receiptAllocation)
        {
            decimal sum = 0;
            if (receiptAllocation.Receives != null)
                foreach (Receive r in receiptAllocation.Receives)
                {
                    foreach (ReceiveDetail rd in r.ReceiveDetails)
                    {
                        sum = sum + Math.Abs(rd.QuantityInUnit);
                    }
                }
            return sum;
        }
        //public List<ReceiptAllocation> GetUnclosedAllocations(int hubId, int? type)
        //{
        //    var unclosed = (from rAll in db.ReceiptAllocations
        //                    where rAll.IsClosed == false
        //                          && hubId == rAll.HubID
        //                          && rAll.CommoditySourceID == type
        //                    select rAll);
        //    if (type != null)
        //    {
        //        unclosed = unclosed.Where(p => p.CommoditySourceID == type.Value);
        //    }

        //    foreach (ReceiptAllocation receiptAllocation in unclosed)
        //    {
        //        int si = repository.ShippingInstruction.GetShipingInstructionId(receiptAllocation.SINumber);
        //        receiptAllocation.RemainingBalanceInMt = receiptAllocation.QuantityInMT -
        //            Math.Abs(repository.Transaction.GetTotalReceivedFromReceiptAllocation(si, receiptAllocation.CommodityID, hubId));
        //        receiptAllocation.CommodityName = receiptAllocation.Commodity.Name;

        //    }

        //    return unclosed.ToList();

        //}

        public void CloseById(Guid id)
        {
            var delAllocation = db.ReceiptAllocations.FirstOrDefault(allocation => allocation.ReceiptAllocationID == id);
            if (delAllocation != null) delAllocation.IsClosed = true;
            db.SaveChanges();
        }


        //public List<ReceiptAllocation> GetAllByType(int commoditySourceType)
        //{
        //    return db.ReceiptAllocations.Where(p => p.CommoditySourceID == commoditySourceType).ToList();
        //}


        public List<string> GetSIsWithOutGiftCertificate(int commoditySoureType)
        {
            var x = GetListOfSource(commoditySoureType);

            var query = (from q in db.ReceiptAllocations
                         where q.GiftCertificateDetail == null && x.Any(p1=>p1 == q.CommoditySourceID) 
                         select q.SINumber).Distinct();
           
            return query.ToList();     
        }


        public List<ReceiptAllocation> GetAllByTypeMerged(int commoditySoureType)
        {
            var x = new int[] { };

            if (commoditySoureType == BLL.CommoditySource.Constants.DONATION)//TODO remove this Condition line later
            {
                x = new int[] { BLL.CommoditySource.Constants.DONATION };
            }
            else if (commoditySoureType == BLL.CommoditySource.Constants.LOCALPURCHASE)
            {
                x = new int[] { BLL.CommoditySource.Constants.LOCALPURCHASE };
            }
            //else if (commoditySoureType == BLL.CommoditySource.Constants.TRANSFER)
            //{
            //    x = new int[] { BLL.CommoditySource.Constants.TRANSFER };
            //}
            else if (BLL.CommoditySource.Constants.REPAYMENT == commoditySoureType || 
                commoditySoureType == BLL.CommoditySource.Constants.TRANSFER ||
                BLL.CommoditySource.Constants.LOAN == commoditySoureType ||
                BLL.CommoditySource.Constants.SWAP == commoditySoureType)
            {
                x = new int[]{  BLL.CommoditySource.Constants.REPAYMENT, 
                                BLL.CommoditySource.Constants.LOAN, 
                                BLL.CommoditySource.Constants.SWAP,
                                BLL.CommoditySource.Constants.TRANSFER};
            }


            var query = (from q in db.ReceiptAllocations
                         where x.Any(p1 => p1 == q.CommoditySourceID)
                         select q);

            return query.ToList();
        }

        public bool IsSINSource(int source, string siNumber)
        {
            var query = (from q in db.ReceiptAllocations
                         where q.SINumber == siNumber && q.CommoditySourceID == source
                         select q);

            return query.Any();
        }


        public List<Commodity> GetAvailableCommoditiesFromUnclosed(string SINumber, int hubId, int? commoditySourceId)
        {
            var query = (from q in db.ReceiptAllocations
                         where q.SINumber == SINumber && q.IsClosed == false && q.HubID == hubId && q.CommoditySourceID == commoditySourceId
                         select q.Commodity).Distinct();
            
            return query.ToList();
        }

        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        public List<ReceiptAllocation> GetSIBalances(string SINumber)
        {
            var list = (from RA in db.ReceiptAllocations
                        where RA.IsClosed == false && RA.SINumber == SINumber
                        select RA).ToList();

            return list;
        }

        public List<SIBalance> GetSIBalanceForCommodity(int hubId, int CommodityId)
        {
            var list = (from RA in db.ReceiptAllocations
                        where
                            (RA.IsClosed == false && RA.HubID == hubId) &&
                            (RA.Commodity.ParentID == CommodityId || RA.CommodityID == CommodityId) 
                            group RA by new { RA.SINumber,RA.ProjectNumber, RA.Commodity,RA.Program} into si
                            select new SIBalance
                                   {
                                       AvailableBalance = 0,
                                       CommitedToFDP = 0,
                                       CommitedToOthers = 0,
                                       Commodity = si.Key.Commodity.Name,
                                       Dispatchable = si.Sum(p => p.QuantityInMT),
                                       SINumber = si.Key.SINumber,
                                       Program = si.Key.Program.Name,
                                       Project = si.Key.ProjectNumber,
                                       ProjectCodeID = 0,
                                       ReaminingExpectedReceipts = si.Sum(p => p.QuantityInMT),//RA.QuantityInMT,
                                       TotalDispatchable = 0,//si.Sum(p => p.QuantityInMT),
                                       
                                   }).ToList();

             
            foreach (var siBalance in list)
            {
                var sis = repository.ShippingInstruction.GetShipingInstructionId(siBalance.SINumber);
               //siBalance.ReaminingExpectedReceipts     = totalSumForComm; 

                //decimal totalSumForComm = (from rAll in db.ReceiptAllocations
                //                           where rAll.IsClosed == false && rAll.SINumber == siBalance.SINumber
                //                                 && rAll.CommodityID == CommodityId ||
                //                                 rAll.Commodity.ParentID == CommodityId
                //                           select rAll.QuantityInMT).Sum();
                //siBalance.ReaminingExpectedReceipts = totalSumForComm;


                if (sis != 0)
                {
                    //siBalance.SINumberID = sis;
                    //var correctedBalance = repository.ShippingInstruction.GetBalance(hubId, CommodityId, sis);

                    //if()
                    //    repository.ReceiptAllocation.GetReceivedAlready()
                        //list.Sum(p => p.ReaminingExpectedReceipts);
                    
                    
                    //siBalance.SINumberID);
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.TotalDispatchable = correctedBalance.TotalDispatchable;
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.ReaminingExpectedReceipts = correctedBalance.ReaminingExpectedReceipts;
                    int commodityId = repository.Commodity.FindById(CommodityId).ParentID ?? CommodityId;
                    ShippingInstruction si = repository.ShippingInstruction.FindById(sis);
                    // convert the amount which is in Quintals to ... MT
                    siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                               where v.IsClosed == false && v.CommodityID == commodityId
                                               select v.Amount / 10).DefaultIfEmpty().Sum();
                    var utilGetDispatchedAllocationFromSiResult = db.util_GetDispatchedAllocationFromSI(hubId, sis).FirstOrDefault();
                    if (utilGetDispatchedAllocationFromSiResult != null)
                    {
                        if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                            siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;
                    }

               
                    siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                                  where v.IsClosed == false && v.CommodityID == commodityId
                                                  select v.QuantityInMT).DefaultIfEmpty().Sum();
                    //sum up all the Expected reamining quantities
                    //siBalance.ReaminingExpectedReceipts = siBalance.ReaminingExpectedReceipts;
                    siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + siBalance.ReaminingExpectedReceipts;

                    //TODO if(siBalance.CommitedToFDP + siBalance.CommitedToOthers == 0 )//set total to 0
                    if ((siBalance.CommitedToFDP + siBalance.CommitedToOthers) == 0)
                        siBalance.TotalDispatchable = 0;
                    else
                        siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                                  (siBalance.CommitedToFDP + siBalance.CommitedToOthers);

                }
            }

            return list;
            //return null;
        }


        public List<SIBalance> GetSIBalanceForCommodityInUnit(int hubId, int CommodityId)
        {
            var list = (from RA in db.ReceiptAllocations
                        where
                            (RA.IsClosed == false && RA.HubID == hubId) &&
                            (RA.Commodity.ParentID == CommodityId || RA.CommodityID == CommodityId)
                        group RA by new { RA.SINumber, RA.ProjectNumber, RA.Commodity, RA.Program } into si
                        select new SIBalance
                        {
                            AvailableBalance = 0,
                            CommitedToFDP = 0,
                            CommitedToOthers = 0,
                            Commodity = si.Key.Commodity.Name,
                            Dispatchable = si.Sum(p => p.QuantityInUnit ?? 0),
                                    //                  {
                                    //                      if (p.QuantityInUnit == null)
                                    //return 0;
                                    //                      return p.QuantityInUnit.Value;
                                    //                  }),
                            SINumber = si.Key.SINumber,
                            Program = si.Key.Program.Name,
                            Project = si.Key.ProjectNumber,
                            ProjectCodeID = 0,
                            ReaminingExpectedReceipts = si.Sum(p => p.QuantityInUnit ?? 0),
                                    //                               {
                                    //                                   if (p.QuantityInUnit == null)
                                    //return 0;
                                    //                                   return p.QuantityInUnit.Value;
                                    //                               }),
                            TotalDispatchable = 0,//si.Sum(p => p.QuantityInMT),

                        }).ToList();


            foreach (var siBalance in list)
            {
                var sis = repository.ShippingInstruction.GetShipingInstructionId(siBalance.SINumber);
                //siBalance.ReaminingExpectedReceipts     = totalSumForComm; 

                //decimal totalSumForComm = (from rAll in db.ReceiptAllocations
                //                           where rAll.IsClosed == false && rAll.SINumber == siBalance.SINumber
                //                                 && rAll.CommodityID == CommodityId ||
                //                                 rAll.Commodity.ParentID == CommodityId
                //                           select rAll.QuantityInMT).Sum();
                //siBalance.ReaminingExpectedReceipts = totalSumForComm;


                if (sis != 0)
                {
                    //siBalance.SINumberID = sis;
                    //var correctedBalance = repository.ShippingInstruction.GetBalance(hubId, CommodityId, sis);

                    //if()
                    //    repository.ReceiptAllocation.GetReceivedAlready()
                    //list.Sum(p => p.ReaminingExpectedReceipts);


                    //siBalance.SINumberID);
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.TotalDispatchable = correctedBalance.TotalDispatchable;
                    //siBalance.Dispatchable = correctedBalance.Dispatchable;
                    //siBalance.ReaminingExpectedReceipts = correctedBalance.ReaminingExpectedReceipts;
                    int commodityId = repository.Commodity.FindById(CommodityId).ParentID ?? CommodityId;
                    ShippingInstruction si = repository.ShippingInstruction.FindById(sis);
                    // convert the amount which is in Quintals to ... MT
                    siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                               where v.IsClosed == false && v.CommodityID == commodityId
                                               select v.AmountInUnit).DefaultIfEmpty().Sum();
                    var utilGetDispatchedAllocationFromSiResult = db.util_GetDispatchedAllocationFromSI(hubId, sis).FirstOrDefault();
                    if (utilGetDispatchedAllocationFromSiResult != null)
                        if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                            siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;

                    siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                                  where v.IsClosed == false && v.CommodityID == commodityId
                                                  select v.QuantityInUnit).DefaultIfEmpty().Sum();
                    //sum up all the Expected reamining quantities
                    //siBalance.ReaminingExpectedReceipts = siBalance.ReaminingExpectedReceipts;
                    siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + siBalance.ReaminingExpectedReceipts;

                    //TODO if(siBalance.CommitedToFDP + siBalance.CommitedToOthers == 0 )//set total to 0
                    if ((siBalance.CommitedToFDP + siBalance.CommitedToOthers) == 0)
                        siBalance.TotalDispatchable = 0;
                    else
                        siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                                  (siBalance.CommitedToFDP + siBalance.CommitedToOthers);

                }
            }

            return list;
            //return null;
        }


    }
}
