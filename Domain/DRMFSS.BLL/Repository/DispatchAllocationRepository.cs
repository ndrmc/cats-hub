using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL.ViewModels.Common;
using DRMFSS.BLL.ViewModels.Dispatch;
using System;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DispatchAllocationRepository : IDispatchAllocationRepository
    {
        /// <summary>
        /// Gets the balance of an SI number commodity .
        /// </summary>
        /// <param name="siNumber">The si number.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public decimal GetUncommitedBalance(int siNumber, int commodityId, int hubId)
        {
            var total = (from tr in db.Transactions
                             where tr.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                         && tr.ShippingInstructionID == siNumber && (tr.CommodityID == commodityId || tr.ParentCommodityID == commodityId) && tr.HubID == hubId
                         select tr.QuantityInMT);
            if(total.Any())
            {
                return total.Sum();
            }
            return 0;
        }

        /// <summary>
        /// Gets the list of dispatch allocations uncommited allocations by hub.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetUncommitedAllocationsByHub(int hubId)
        {
            return db.DispatchAllocations.Where(p => p.HubID == hubId && !p.ShippingInstructionID.HasValue && !p.ProjectCodeID.HasValue).ToList();
        }

        /// <summary>
        /// Gets the list of uncommited uncomited allocations.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetUncomitedAllocations(Guid[] ids)
        {
            return db.DispatchAllocations.Where(p => ids.Contains(p.DispatchAllocationID)).ToList();
        }

        /// <summary>
        /// Gets the allocations.
        /// </summary>
        /// <param name="RequisitionNo">The requisition no.</param>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="hubId">The hub id.</param>
        /// <param name="UnComitted">if set to <c>true</c> [un comitted].</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetAllocations(string RequisitionNo, int CommodityID, int hubId, bool UnComitted, string PreferedWeightMeasurment)
        {
            var list =  db.DispatchAllocations.Where(p => p.RequisitionNo == RequisitionNo && p.HubID == hubId 
                && p.CommodityID == CommodityID);
            if (UnComitted)
            {
                list = list.Where(p => !p.ShippingInstructionID.HasValue && !p.ProjectCodeID.HasValue);
            }
            foreach (var dispatchAllocation in list)
            {
                //dispatchAllocation.AmontInUnit = dispatchAllocation.Amount;TODO if we were 
                if (PreferedWeightMeasurment.ToUpperInvariant() == "MT" && dispatchAllocation.Commodity.CommodityTypeID == 1)
                {
                    dispatchAllocation.Amount /= 10;
                }
            }
          
            return list.ToList();
        }

        /// <summary>
        /// Gets the allocations.
        /// </summary>
        /// <param name="RequisitionNo">The requisition no.</param>
        /// <param name="hubId">The hub id.</param>
        /// <param name="UnComitted">if set to <c>true</c> [un comitted].</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetAllocations(string RequisitionNo, int hubId, bool UnComitted)
        {
            var list = db.DispatchAllocations.Where(p => p.RequisitionNo == RequisitionNo && p.HubID == hubId);
            if (UnComitted)
            {
                list = list.Where(p => !p.ShippingInstructionID.HasValue && !p.ProjectCodeID.HasValue);
            }
            return list.ToList();
        }

        /// <summary>
        /// Gets the available SI numbers in a given hub
        /// </summary>
        /// <param name="commodityID">The commodity ID.</param>
        /// <param name="hubID"> </param>
        /// <returns></returns>
        public List<ShippingInstruction> GetAvailableSINumbersWithUncommitedBalance(int commodityID, int hubID)
        {
            var SIs = (from tr in db.Transactions
                       where (tr.CommodityID == commodityID || tr.ParentCommodityID == commodityID)
                             && tr.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                             && tr.HubID == hubID
                       group tr by new {tr.ShippingInstruction}
                       into si
                       select new {SI = si.Key.ShippingInstruction, AvailableBalance = si.Sum(p => p.QuantityInMT ), AvailableBalanceInUnit = si.Sum(q=>q.QuantityInUnit)});

            return (from si in SIs
                    where si.AvailableBalance > 0 || si.AvailableBalanceInUnit > 0
                    select si.SI).ToList();
        }

        /// <summary>
        /// Gets the uncommited allocation transaction.
        /// </summary>
        /// <param name="commodityID">The commodity ID.</param>
        /// <param name="shipingInstructionID">The shiping instruction ID.</param>
        /// <param name="hubID">The hub ID.</param>
        /// <returns></returns>
        public Transaction GetUncommitedAllocationTransaction(int commodityID, int shipingInstructionID, int hubID)
        {
            return (from tr in db.Transactions
                    where (tr.CommodityID == commodityID || tr.ParentCommodityID == commodityID) && tr.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED 
                    && tr.ShippingInstructionID == shipingInstructionID && tr.HubID == hubID
                    select tr).FirstOrDefault();
        }

        /// <summary>
        /// Gets the available commodities.
        /// </summary>
        /// <param name="requisitionNo">The requisition no.</param>
        /// <returns></returns>
        public List<Commodity> GetAvailableCommodities(string requisitionNo)
        {
            return (from tr in db.DispatchAllocations
                    where tr.RequisitionNo == requisitionNo
                    select tr.Commodity).Distinct().ToList();
        }

        /// <summary>
        /// Attaches the transaction group.
        /// </summary>
        /// <param name="allocation">The allocation.</param>
        /// <param name="TransactionGroupID">The transaction group ID.</param>
        /// <returns></returns>
        public bool AttachTransactionGroup(DispatchAllocation allocation, int TransactionGroupID)
        {
            DispatchAllocation original = db.DispatchAllocations.Where(p => p.DispatchAllocationID == allocation.DispatchAllocationID).SingleOrDefault();
            if (original != null)
            {
                //original.TransactionGroupID = TransactionGroupID;
                db.SaveChanges();
                return true;
            }
            return false;

        }

        /// <summary>
        /// Gets the SI balances.
        /// </summary>
        /// <returns></returns>
        public List<BLL.SIBalance> GetSIBalances(int hubID)
        {
            var list = (from ls in db.Transactions
                        where ls.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && ls.HubID == hubID
                        group ls by new { ls.ShippingInstruction, ls.ProjectCode, ls.Commodity, ls.Program } into si
                        select new SIBalance() { SINumber = si.Key.ShippingInstruction.Value, 
                            AvailableBalance = si.Sum(p => p.QuantityInMT) ,
                            Commodity = si.Key.Commodity.Name,
                            Project = si.Key.ProjectCode.Value,
                            Program = si.Key.Program.Name,
                            SINumberID = si.Key.ShippingInstruction.ShippingInstructionID,
                            ProjectCodeID = si.Key.ProjectCode.ProjectCodeID
                        }).ToList();

           return list;
        }

        /// <summary>
        /// Gets the SI balances grouped by commodity.
        /// </summary>
        /// <returns></returns>
        public List<BLL.CommodityBalance> GetSIBalancesGroupedByCommodity(int hubID)
        {
            var list = (from ls in GetSIBalances(hubID)
                        group ls by new { ls.Commodity} into si
                        select new CommodityBalance()
                        {
                            Commodity = si.Key.Commodity,
                            Balances = si.ToList()
                            
                        }).ToList();

            return list;
        }



        public bool CommitDispatchAllocation(Guid AllocationId, int SIID, int ProjectCodeID)
        {
            DispatchAllocation all = db.DispatchAllocations.Where(p => p.DispatchAllocationID == AllocationId).SingleOrDefault();
            if (all != null)
            {
                all.ShippingInstructionID = SIID;
                all.ProjectCodeID = ProjectCodeID;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the commited allocations by hub.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetCommitedAllocationsByHub(int hubId)
        {
            return db.DispatchAllocations.Where(p => p.HubID == hubId && p.ShippingInstructionID.HasValue && p.ProjectCodeID.HasValue).ToList();
        }

        public List<DispatchAllocationViewModelDto> GetCommitedAllocationsByHubDetached(int hubId, string PreferedWeightMeasurment, bool? closedToo, int? AdminUnitId, int? CommodityType)
        {
            List<DispatchAllocationViewModelDto> GetUncloDetacheced = new List<DispatchAllocationViewModelDto>();

            var unclosed = (from dAll in db.DispatchAllocations
                            where dAll.ShippingInstructionID.HasValue && dAll.ProjectCodeID.HasValue
                                  && hubId == dAll.HubID
            
                select dAll);
            
            if(AdminUnitId.HasValue)
            {
                BLL.AdminUnit adminunit = repository.AdminUnit.FindById(AdminUnitId.Value);
                
                if(adminunit.AdminUnitType.AdminUnitTypeID == 2)//by region
                    unclosed =
                        unclosed.Where(p => p.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == AdminUnitId.Value);
                else if(adminunit.AdminUnitType.AdminUnitTypeID == 3)//by zone
                    unclosed =
                            unclosed.Where(p => p.FDP.AdminUnit.AdminUnit2.AdminUnitID == AdminUnitId.Value);
                else if(adminunit.AdminUnitType.AdminUnitTypeID == 4)//by woreda
                    unclosed =
                            unclosed.Where(p => p.FDP.AdminUnit.AdminUnitID == AdminUnitId.Value);
                //DAVMD.Region = adminunit.FDP.AdminUnit.AdminUnit2.AdminUnit2.;
                //DAVMD.Zone = adminunit.FDP.AdminUnit.AdminUnit2.Name;
                //DAVMD.Woreda = adminunit.FDP.AdminUnit.Name;

                //unclosed = unclosed.Where(p => p.FDP.AdminUnitID == AdminUnitId.Value);
            }
            if (closedToo == null || closedToo == false)
            {
                unclosed = unclosed.Where(p => p.IsClosed == false);
            }
            else
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
   
            foreach (var dispatchAllocation in unclosed)
            {
                var DAVMD = new DispatchAllocationViewModelDto();
                if(PreferedWeightMeasurment.ToUpperInvariant() == "MT" && dispatchAllocation.Commodity.CommodityTypeID == 1) //only for food
                {
                    DAVMD.Amount = dispatchAllocation.Amount/10;
                    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount/10;
                    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals/10;
                }else
                {
                    DAVMD.Amount = dispatchAllocation.Amount;
                    DAVMD.DispatchedAmount = dispatchAllocation.DispatchedAmount;
                    DAVMD.RemainingQuantityInQuintals = dispatchAllocation.RemainingQuantityInQuintals;
                }
                    DAVMD.DispatchAllocationID = dispatchAllocation.DispatchAllocationID;
                    DAVMD.CommodityName = dispatchAllocation.Commodity.Name;
                    DAVMD.RequisitionNo = dispatchAllocation.RequisitionNo;
                    DAVMD.BidRefNo = dispatchAllocation.BidRefNo;

                    DAVMD.Region = dispatchAllocation.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                    DAVMD.Zone = dispatchAllocation.FDP.AdminUnit.AdminUnit2.Name;
                    DAVMD.Woreda = dispatchAllocation.FDP.AdminUnit.Name;
                    DAVMD.FDPName = dispatchAllocation.FDP.Name;
                    DAVMD.IsClosed = dispatchAllocation.IsClosed;


                   DAVMD.AmountInUnit = DAVMD.Amount;
                    DAVMD.DispatchedAmountInUnit = dispatchAllocation.DispatchedAmountInUnit;
                    DAVMD.RemainingQuantityInUnit = dispatchAllocation.RemainingQuantityInUnit;

                GetUncloDetacheced.Add(DAVMD);
               // db.Detach(dispatchAllocation);
            }
            return GetUncloDetacheced;

        }

        public List<BidRefViewModel> GetAllBidRefsForReport()
        {
            var BidRefs = (from b in db.DispatchAllocations where b.BidRefNo != string.Empty select new ViewModels.Common.BidRefViewModel() { BidRefId = b.BidRefNo, BidRefText = b.BidRefNo }).Distinct().ToList();
            return BidRefs;
        }

        /// <summary>
        /// Gets the available requision numbers.
        /// </summary>
        /// <param name="HubId">The hub id.</param>
        /// <param name="UnCommited">if set to <c>true</c> [un commited].</param>
        /// <returns></returns>
        public List<string> GetAvailableRequisionNumbers(int HubId, bool UnCommited)
        {
            var list =  (from allocation in db.DispatchAllocations
                         where allocation.HubID == HubId
                    select allocation);
            if (UnCommited)
            {
                list = list.Where(p => !p.ShippingInstructionID.HasValue && !p.ProjectCodeID.HasValue);
            }

            return list.Select(p => p.RequisitionNo).Distinct().ToList();

        }


        /// <summary>
        /// Gets the allocations under a given requisition number.
        /// </summary>
        /// <param name="requisitonNumber">The requisiton number.</param>
        /// <returns></returns>
        public List<DispatchAllocation> GetAllocations(string requisitonNumber)
        {
            return (from v in db.DispatchAllocations
                    where v.RequisitionNo == requisitonNumber
                    select v).ToList();

        }


        /// <summary>
        /// Commits the dispatch allocation.
        /// </summary>
        /// <param name="checkedRecords">The checked records.</param>
        /// <param name="SINumber">The SI number.</param>
        /// <param name="user">The user.</param>
        /// <param name="ProjectCode">The project code.</param>
        public void CommitDispatchAllocation(string[] checkedRecords, int SINumber, UserProfile user, int ProjectCode)
        {
            foreach (var checkedRecord in checkedRecords)
            {
                CommitDispatchAllocation(Guid.Parse(checkedRecord), SINumber, ProjectCode);
            }
            
        }


        public List<ViewModels.Dispatch.RequisitionSummary> GetSummaryForUncommitedAllocations(int hubId)
        {
            var UnCommitedDispatches = (from v in db.DispatchAllocations
                              where v.IsClosed == false && v.ProgramID == null && v.Year == null
                              select new RequisitionSummary {CommodityName = v.Commodity.Name, Region = v.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name,RequistionNo = v.RequisitionNo,Status = "Not Commited", Zone =v.FDP.AdminUnit.AdminUnit2.Name}
                             ).Distinct().ToList();
            return UnCommitedDispatches;
        }

        public List<ViewModels.Dispatch.RequisitionSummary> GetSummaryForCommitedAllocations(int hubId)
        {
            var CommitedDispatches = (from v in db.DispatchAllocations
                                        where v.IsClosed != false && v.ProgramID != null && v.Year != null
                                        select new RequisitionSummary { CommodityName = v.Commodity.Name, Region = v.FDP.AdminUnit.AdminUnit2.AdminUnit2.Name, RequistionNo = v.RequisitionNo, Status = "Not Commited", Zone = v.FDP.AdminUnit.AdminUnit2.Name }
                             ).Distinct().ToList();
            return CommitedDispatches;
        }


        /// <summary>
        /// Gets the uncommited SI balance.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <returns></returns>
        public List<SIBalance> GetUncommitedSIBalance(int hubID, int commodityId, string PreferedWeightMeasurment)
        {
            var sis = repository.ShippingInstruction.GetShippingInstructionsWithBalance(hubID, commodityId);
            var com = repository.Commodity.FindById(commodityId);
            bool IsFood = com.CommodityTypeID == 1;
            List<SIBalance> result = new List<SIBalance>();
            List<SIBalance> positiveresult = new List<SIBalance>();
            foreach (var si in sis)
            {
                SIBalance balance = null;

               if(IsFood)
                 balance = repository.ShippingInstruction.GetBalance(hubID, commodityId,
                                                                              si.ShippingInstructionID);
               else
                   balance = repository.ShippingInstruction.GetBalanceInUnit(hubID, commodityId,
                                                                              si.ShippingInstructionID);
                //if (balance.Dispatchable > 0)//buggy if the in store balance is less than 0 it will be replaced by the data by receipt allocation data
                if(balance!= null)
                    result.Add(balance);
            }


            //From the receipt allocation
            List<SIBalance> SIfromReceipts = new List<SIBalance>();
             if(IsFood)
                 SIfromReceipts = repository.ReceiptAllocation.GetSIBalanceForCommodity(hubID, commodityId);
             else
                 SIfromReceipts = repository.ReceiptAllocation.GetSIBalanceForCommodityInUnit(hubID, commodityId);

            foreach (var sIfromReceipt in SIfromReceipts)
            {
                if (!result.Any(p => p.SINumber == sIfromReceipt.SINumber) && sIfromReceipt.Dispatchable > 0)
                {
                    result.Add(sIfromReceipt);
                }
            }
            foreach (SIBalance siBalanceList in result)
            {
                if (siBalanceList.Dispatchable > 0)
                {
                    if(PreferedWeightMeasurment.ToUpperInvariant() == "QN" && (IsFood))
                    {
                        siBalanceList.AvailableBalance *= 10;
                        siBalanceList.TotalDispatchable *= 10;
                        siBalanceList.Dispatchable *= 10;
                        siBalanceList.ReaminingExpectedReceipts *= 10;
                    }
                    positiveresult.Add(siBalanceList);
                }
            }
            //foreach (var si in sis) {

            //var rAll  = repository.ReceiptAllocation.FindBySINumber(si.Value)
            //    .Where(
            //    p =>
            //    {
            //        if (p.Commodity.ParentID == null)
            //            return p.CommodityID == commodityId;
            //        else
            //            return p.Commodity.ParentID == commodityId;
            //    }
            //    )
            //    .Where(q=>q.IsClosed == false).Select( new SIBalance
            //                                               {
            //                                                   AvailableBalance = 
            //                                               });

           //}

        //foreach (var si in sis)
            //{
            //    if (si != null)
            //    {
            //        repository.ReceiptAllocation.GetAll().Where(
            //            p => p.SINumber == si.Value && p.CommodityID == commodityId);

            //    }
            //}

            return positiveresult;
        }


        public void CloseById(Guid id)
        {
            var delAllocation = db.DispatchAllocations.FirstOrDefault(allocation => allocation.DispatchAllocationID == id);
            if (delAllocation != null) 
                delAllocation.IsClosed = true;
            db.SaveChanges();
        }


    }
}
