using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.ViewModels;
using DRMFSS.Web.Models;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DispatchRepository :GenericRepository<CTSContext,Dispatch>, IDispatchRepository
    {
        public DispatchRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public Dispatch GetDispatchByGIN(string ginNo)
        {
            return db.Dispatches.FirstOrDefault(p => p.GIN == ginNo);
        }

        /// <summary>
        /// Gets the dispatch transaction.
        /// </summary>
        /// <param name="dispatchId">The dispatch id.</param>
        /// <returns></returns>
        public Transaction GetDispatchTransaction(Guid dispatchId)
        {
            var transactionGroup = (from d in db.DispatchDetails
                                    where d.DispatchID == dispatchId
                                    select d.TransactionGroup).FirstOrDefault();
            if (transactionGroup != null && transactionGroup.Transactions.Count > 0)
            {
                return transactionGroup.Transactions.First();
            }
            return null;
        }

        /// <summary>
        /// Gets the FDP balance.
        /// </summary>
        /// <param name="FDPID">The FDPID.</param>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public FDPBalance GetFDPBalance(int FDPID, string RequisitionNo)
        {
            var query = from q in db.DispatchAllocations
                        where q.FDPID == FDPID && q.RequisitionNo == RequisitionNo
                        select q;

            FDPBalance balance = new FDPBalance();
            if (query.Count() > 0)
            {
                var total = query.Select(t => t.Amount);
                if (total.Count() > 0)
                {
                    balance.TotalAllocation = total.Sum();
                }
                var commited = query.Where(p => p.ShippingInstructionID.HasValue && p.ProjectCodeID.HasValue).Select(t => t.Amount);
                if(commited.Count() > 0)
                {
                    balance.CommitedAllocation = Math.Abs(commited.Sum());
                }
                var transaction = query.FirstOrDefault();
                balance.CommodityTypeID = transaction.Commodity.CommodityTypeID;
                balance.ProgramID = (transaction.ProgramID.HasValue)?transaction.ProgramID.Value:-1;
                balance.Commodity = transaction.Commodity.Name;
                balance.ProjectCode = (transaction.ProjectCodeID.HasValue)?transaction.ProjectCode.Value:"Not Applicable";
                // find more details from the dispatch allocation table.
                // TOCHECK: check if this is woring correctly
                //if(transaction.TransactionGroup.DispatchAllocations.Any())
                //{
                //    DispatchAllocation dispatchAllocation =
                //        transaction.TransactionGroup.DispatchAllocations.FirstOrDefault();
                //    balance.BidNumber = dispatchAllocation.BidRefNo;
                //    balance.TransporterID = dispatchAllocation.TransporterID.Value;
                //}
                var transactions = (from v in db.Dispatches
                                    where v.RequisitionNo == RequisitionNo && v.FDPID == FDPID
                                    select v.DispatchDetails.FirstOrDefault().TransactionGroup.Transactions);
                List<Transaction> trans = new List<Transaction>();
                foreach (var tran in transactions)
                {
                    if (tran != null)
                    {
                        foreach (var t in tran)
                        {
                            trans.Add(t);
                        }
                    }

                }


                if (balance.CommodityTypeID == 1)
                {
                    balance.TotalDispatchedMT = (from v in trans
                                                 where v.LedgerID == Ledger.Constants.GOODS_DISPATCHED
                                                 select v.QuantityInMT).DefaultIfEmpty().Sum();

                }else
                {
                    balance.TotalDispatchedMT = (from v in trans
                                                 where v.LedgerID == Ledger.Constants.GOODS_DISPATCHED
                                                 select v.QuantityInUnit).DefaultIfEmpty().Sum();
                    
                }

                // Convert the MT to Quintal,
                balance.CurrentBalance = balance.TotalAllocation - (balance.TotalDispatchedMT * 10);
            }
            
            return balance;
        }

        /// <summary>
        /// Gets the available commodities.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        public List<Commodity> GetAvailableCommodities(string SINumber, int hubID)
        {
            var query = (from q in db.Transactions
                        where q.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                        && q.ShippingInstruction.Value == SINumber && q.HubID == hubID
                        select q.Commodity).Distinct();
            return query.ToList();
        }



        public List<Web.Models.DispatchModelModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid dispatchAllocationId)
        {
            List<DispatchModelModelDto> dispatchs = new List<DispatchModelModelDto>();

            var query = (from r in db.Dispatches
                         where r.HubID == hubId && r.DispatchAllocationID == dispatchAllocationId
                         select new DispatchModelModelDto()
                         {
                             DispatchDate = r.DispatchDate,
                             GIN = r.GIN,
                             DispatchedByStoreMan = r.DispatchedByStoreMan,
                             DispatchID = r.DispatchID
                         });

            return query.ToList();
        }

        public List<Web.Models.DispatchModelModelDto> ByHubIdAndOtherAllocationIDetached(int hubId, Guid OtherDispatchAllocationId)
        {
            List<DispatchModelModelDto> dispatchs = new List<DispatchModelModelDto>();

            var query = (from r in db.Dispatches
                         where r.HubID == hubId && r.OtherDispatchAllocationID == OtherDispatchAllocationId
                         select new DispatchModelModelDto()
                         {
                             DispatchDate = r.DispatchDate,
                             GIN = r.GIN,
                             DispatchedByStoreMan = r.DispatchedByStoreMan,
                             DispatchID = r.DispatchID
                         });

            return query.ToList();
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Dispatches.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Dispatch FindById(int id)
        {
           return null;
        }

        public Dispatch FindById(System.Guid id)
        { return db.Dispatches.FirstOrDefault(t => t.DispatchID == id);
            

        }
    }
}
