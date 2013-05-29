using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.ViewModels.Common;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ShippingInstructionRepository :GenericRepository<CTSContext,ShippingInstruction>, IShippingInstructionRepository
    {
        public ShippingInstructionRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// Gets the shiping instruction id.
        /// </summary>
        /// <param name="si">The si.</param>
        /// <returns></returns>
        public int GetShipingInstructionId(string si)
        {
            ShippingInstruction instruction = (from i in db.ShippingInstructions
                                               where i.Value.ToUpper() == si.ToUpper()
                                               select i).SingleOrDefault();
            if (instruction != null)
            {
                return instruction.ShippingInstructionID;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the specified si number has balance.
        /// </summary>
        /// <param name="SiNumber">The si number.</param>
        /// <param name="FDPID">The FDPID.</param>
        /// <returns>
        ///   <c>true</c> if the specified si number has balance; otherwise, <c>false</c>.
        /// </returns>
        public bool HasBalance(string SiNumber, int FDPID)
        {

            var balance = (from si in db.Transactions
                           where si.ShippingInstruction.Value == SiNumber &&
                                 si.Account.EntityType == Account.Constants.FDP && si.Account.EntityID == FDPID
                           select si.QuantityInMT).Sum();
            return (balance > 0);
        }


        /// <summary>
        /// Gets the SI number id With create.
        /// </summary>
        /// <param name="SiNumber">The si number.</param>
        /// <returns></returns>
        public ShippingInstruction GetSINumberIdWithCreate(string SiNumber)
        {
            ShippingInstruction instruction = (from i in db.ShippingInstructions
                                               where i.Value.ToUpper() == SiNumber.ToUpper()
                                               select i).SingleOrDefault();
            if (instruction != null)
            {
                return instruction;
            }
            else
            {
                ShippingInstruction newInstruction = new ShippingInstruction()
                                                         {
                                                             Value = SiNumber.ToUpperInvariant()
                                                         };
                db.ShippingInstructions.Add(newInstruction);
                db.SaveChanges();
                return newInstruction;
            }
        }


        public List<ViewModels.Common.ShippingInstructionViewModel> GetAllShippingInstructionForReport()
        {
            var shippingInstructions = (from c in db.ShippingInstructions
                                        select
                                            new ViewModels.Common.ShippingInstructionViewModel()
                                                {
                                                    ShippingInstructionId = c.ShippingInstructionID,
                                                    ShippingInstructionName = c.Value
                                                }).ToList();
            return shippingInstructions;
        }

        public List<ViewModels.Common.ShippingInstructionViewModel> GetShippingInstructionsForProjectCode(int hubId,
                                                                                                          int
                                                                                                              projectCodeId)
        {
            var shippingInstructions = (from v in db.Transactions
                                        where v.HubID == hubId && v.ProjectCodeID == projectCodeId
                                        select
                                            new ShippingInstructionViewModel
                                                {
                                                    ShippingInstructionId = v.ShippingInstructionID,
                                                    ShippingInstructionName = v.ShippingInstruction.Value
                                                }).Distinct().
                ToList();
            return shippingInstructions;
        }


        public List<ShippingInstruction> GetShippingInstructionsWithBalance(int hubID, int commodityId)
        {
            var com = repository.Commodity.FindById(commodityId);

            if (com.CommodityTypeID == 1)
            {
                var sis = from v in db.Transactions
                          where
                              v.HubID == hubID && v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED &&
                              v.ParentCommodityID == commodityId
                          group v by v.ShippingInstruction
                          into g
                          select new {g.Key, SUM = g.Sum(b => b.QuantityInMT)};

                return (from v in sis
                        //where v.SUM > 0
                        select v.Key).ToList();
            }else
            {
                var sis = from v in db.Transactions
                          where
                              v.HubID == hubID && v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED &&
                              v.ParentCommodityID == commodityId //non-food should be 
                          group v by v.ShippingInstruction
                              into g
                              select new { g.Key, SUM = g.Sum(b => b.QuantityInUnit) };

                return (from v in sis
                        //where v.SUM > 0
                        select v.Key).ToList();
            }
        }

        public ProjectCode GetProjectCodeForSI(int hubId, int commodityId , int shippingInstructionID)
        {
            var projectCode = (from v in db.Transactions
                              where
                                  v.HubID == hubId && v.ParentCommodityID == commodityId &&
                                  v.ShippingInstructionID == shippingInstructionID
                              select v.ProjectCode).FirstOrDefault();
            return projectCode;

        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="shippingInstructionID">The shipping instruction ID.</param>
        /// <returns></returns>
        public SIBalance GetBalance(int hubID, int commodityId, int shippingInstructionID)
        {
            SIBalance siBalance = new SIBalance();

            siBalance.Commodity = repository.Commodity.FindById(commodityId).Name;
            siBalance.SINumberID = shippingInstructionID;
            
            ProjectCode projectCode = GetProjectCodeForSI(hubID, commodityId, shippingInstructionID);
            siBalance.ProjectCodeID = projectCode.ProjectCodeID;
            siBalance.Project = projectCode.Value;

            ShippingInstruction si = repository.ShippingInstruction.FindById(shippingInstructionID);
            var availableBalance = (from v in si.Transactions
                                    where v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && commodityId == v.ParentCommodityID
                                    select v.QuantityInMT).DefaultIfEmpty().Sum();

            var firstOrDefaultans = si.Transactions.FirstOrDefault();
            if (firstOrDefaultans != null)
                siBalance.Program = firstOrDefaultans.Program.Name;

            siBalance.AvailableBalance = availableBalance;
            siBalance.SINumber = si.Value;

            // convert the amount which is in Quintals to ... MT
            siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                       where v.IsClosed == false && v.CommodityID == commodityId
                                       select v.Amount /10).DefaultIfEmpty().Sum();
            var utilGetDispatchedAllocationFromSiResult = db.util_GetDispatchedAllocationFromSI(hubID, shippingInstructionID).FirstOrDefault();
            if (utilGetDispatchedAllocationFromSiResult != null)
                if (utilGetDispatchedAllocationFromSiResult.Quantity != null)
                    siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.Quantity.Value;

            siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                       where v.IsClosed == false && v.CommodityID == commodityId
                                       select v.QuantityInMT).DefaultIfEmpty().Sum();

            
            
            decimal ReaminingExpectedReceipts = 0;
                var rAll = repository.ReceiptAllocation.FindBySINumber(siBalance.SINumber)
                    .Where(
                    p =>
                    {
                        if (p.Commodity.ParentID == null)
                            return p.CommodityID == commodityId;
                        else
                            return p.Commodity.ParentID == commodityId;
                    }
                    )
                    .Where(q=>q.IsClosed == false);

                foreach (var receiptAllocation in rAll)
                {
                    ReaminingExpectedReceipts = ReaminingExpectedReceipts +
                                       (receiptAllocation.QuantityInMT
                                        -
                                        repository.ReceiptAllocation.GetReceivedAlready(receiptAllocation));
                }
            siBalance.ReaminingExpectedReceipts = ReaminingExpectedReceipts;
            decimal newVariable = (siBalance.CommitedToFDP + siBalance.CommitedToOthers);
            
            siBalance.Dispatchable = siBalance.AvailableBalance - newVariable + ReaminingExpectedReceipts;

            if (newVariable > 0)
                siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                          (siBalance.CommitedToFDP + siBalance.CommitedToOthers);
            else
                siBalance.TotalDispatchable = siBalance.AvailableBalance;
            return siBalance;

        }

        /// <summary>
        /// Gets the balance In Units for non-food items.
        /// </summary>
        /// <param name="hubID">The hub ID.</param>
        /// <param name="commodityId">The commodity id.</param>
        /// <param name="shippingInstructionID">The shipping instruction ID.</param>
        /// <returns></returns>
        public SIBalance GetBalanceInUnit(int hubID, int commodityId, int shippingInstructionID)
        {
            SIBalance siBalance = new SIBalance();

            siBalance.Commodity = repository.Commodity.FindById(commodityId).Name;
            siBalance.SINumberID = shippingInstructionID;

            ProjectCode projectCode = GetProjectCodeForSI(hubID, commodityId, shippingInstructionID);
            siBalance.ProjectCodeID = projectCode.ProjectCodeID;
            siBalance.Project = projectCode.Value;

            ShippingInstruction si = repository.ShippingInstruction.FindById(shippingInstructionID);
            var availableBalance = (from v in si.Transactions
                                    where v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && commodityId == v.ParentCommodityID
                                    select v.QuantityInUnit).DefaultIfEmpty().Sum();

            var firstOrDefaultans = si.Transactions.FirstOrDefault();
            if (firstOrDefaultans != null)
                siBalance.Program = firstOrDefaultans.Program.Name;

            siBalance.AvailableBalance = availableBalance;
            siBalance.SINumber = si.Value;

            // convert the amount which is in Quintals to ... MT
            siBalance.CommitedToFDP = (from v in si.DispatchAllocations
                                       where v.IsClosed == false && v.CommodityID == commodityId
                                       select v.AmountInUnit).DefaultIfEmpty().Sum();
                                       //select v.Amount / 10).DefaultIfEmpty().Sum();
            var utilGetDispatchedAllocationFromSiResult = db.util_GetDispatchedAllocationFromSI(hubID, shippingInstructionID).FirstOrDefault();
            if (utilGetDispatchedAllocationFromSiResult != null)
                if (utilGetDispatchedAllocationFromSiResult.QuantityInUnit != null)
                    siBalance.CommitedToFDP -= utilGetDispatchedAllocationFromSiResult.QuantityInUnit.Value;

            siBalance.CommitedToOthers = (from v in si.OtherDispatchAllocations
                                          where v.IsClosed == false && v.CommodityID == commodityId
                                          select v.QuantityInUnit).DefaultIfEmpty().Sum();



            decimal ReaminingExpectedReceipts = 0;
            var rAll = repository.ReceiptAllocation.FindBySINumber(siBalance.SINumber)
                .Where(
                p =>
                {
                    if (p.Commodity.ParentID == null)
                        return p.CommodityID == commodityId;
                    else
                        return p.Commodity.ParentID == commodityId;
                }
                )
                .Where(q => q.IsClosed == false);

            foreach (var receiptAllocation in rAll)
            {
                decimal Qunt = 0;
                if (receiptAllocation.QuantityInUnit != null)
                    Qunt = receiptAllocation.QuantityInUnit.Value;
                ReaminingExpectedReceipts = ReaminingExpectedReceipts +
                                                (Qunt
                                                 -
                                                 repository.ReceiptAllocation.GetReceivedAlreadyInUnit(receiptAllocation));
            }
            siBalance.ReaminingExpectedReceipts = ReaminingExpectedReceipts;
            siBalance.Dispatchable = siBalance.AvailableBalance - (siBalance.CommitedToFDP + siBalance.CommitedToOthers) + ReaminingExpectedReceipts;
            siBalance.TotalDispatchable = siBalance.AvailableBalance -
                                          (siBalance.CommitedToFDP + siBalance.CommitedToOthers);
            return siBalance;

        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.ShippingInstructions.Remove(original);
            return true;
        }

        public bool DeleteByID(Guid id)
        {
           return  false;
        }

        public ShippingInstruction FindById(int id)
        {
            return db.ShippingInstructions.FirstOrDefault(t => t.ShippingInstructionID == id);
        }

        public ShippingInstruction FindById(Guid id)
        {
            return null;
        }
    }
}


