using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report.Data;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StoreRepository :GenericRepository<CTSContext,Store>, IStoreRepository
    {
        public StoreRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// Gets the store by hub.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<Store> GetStoreByHub(int hubId)
        {
            return db.Stores.Where(p => p.HubID == hubId).ToList();
        }


        public List<int> GetStacksByStoreId(int? StoreId)
        {
            List<int> stacks = new List<int>();

            if (StoreId != null)
            {
                var maxStackNumber =
                    (from c in db.Stores where c.StoreID == StoreId select c.StackCount).FirstOrDefault();
                for (int i = 0; i <= maxStackNumber; i++)
                {
                    stacks.Add(i);
                }
            }
            return stacks;
        }


        public List<Store> GetStoresWithBalanceOfCommodityAndSINumber(int parentCommodityId, int SINumber, int hubId)
        {
            Hub hub = repository.Hub.FindById(hubId);
            List<Store> result = new List<Store>();
            foreach (var store in hub.Stores)
            {
                var balance = (from v in db.Transactions
                               where
                                   v.StoreID == store.StoreID && parentCommodityId == v.ParentCommodityID &&
                                   v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED &&
                                   v.ShippingInstructionID == SINumber
                               select v.QuantityInMT);
                if (balance.Any() && balance.Sum() > 0)
                {
                    result.Add(store);
                }
            }
            return result;
        }

        public List<Store> GetStoresWithBalanceOfCommodity(int parentCommodityId, int hubId)
        {
            Hub hub = repository.Hub.FindById(hubId);
            List<Store> result = new List<Store>();
            foreach (var store in hub.Stores)
            {
                var balance = (from v in db.Transactions
                               where
                                   v.StoreID == store.StoreID && parentCommodityId == v.ParentCommodityID &&
                                   v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED
                               select v.QuantityInMT);
                if (balance.Any() && balance.Sum() > 0)
                {
                    result.Add(store);
                }
            }
            return result;
        }

        public List<int> GetStacksWithSIBalance(int storeId, int siNumber)
        {
            List<int> result = new List<int>();
            Store store = repository.Store.FindById(storeId);
            foreach (var stack in store.Stacks)
            {
                var balance = (from v in db.Transactions
                               where
                                   v.StoreID == store.StoreID && siNumber == v.ShippingInstructionID &&
                                   v.LedgerID == Ledger.Constants.GOODS_ON_HAND_UNCOMMITED && v.Stack == stack
                               select v.QuantityInMT);
                if (balance.Any() && balance.Sum() > 0)
                {
                    result.Add(stack);
                }
            }
            return result;
        }


        public List<int> GetStacksByToStoreIdFromStoreIdFromStack(int ToStoreId, int FromStoreId, int FromStackId)
        {
            List<int> result = new List<int>();
            Store store = repository.Store.FindById(ToStoreId);
            if (ToStoreId == FromStoreId)
            {
                foreach (var stack in store.Stacks)
                {
                    if (stack != FromStackId)
                    {
                        result.Add(stack);
                    }
                }
            }
            else
            {
                foreach (var stack in store.Stacks)
                {
                    result.Add(stack);
                }
            }
            return result;
        }


        public List<Store> GetAllByHUbs(List<Hub> HubIds)
        {
            List<int> hubIds = HubIds.Select(hubId => hubId.HubID).ToList();

            IQueryable<Store> result = (from sT in db.Stores
                          where hubIds.Any(p=>p == sT.HubID)
                          select sT);
            
            return result.ToList();
        }


        public IEnumerable<BinCardViewModel> GetBinCard(int hubID, int? StoreID, int? CommodityID, string ProjectID)
        {
            var commodity = new Commodity();
            if(CommodityID.HasValue)
             commodity = repository.Commodity.FindById(CommodityID.Value);
            List<BinCardReport> results = new List<BinCardReport>();
            if (commodity != null && commodity.CommodityTypeID == 1)
                results = db.RPT_BinCard(hubID, StoreID, CommodityID, ProjectID).ToList();
            else
                results = db.RPT_BinCardNonFood(hubID, StoreID, CommodityID, ProjectID).ToList();
         
            //var results = db.RPT_BinCard(hubID,StoreID,CommodityID,ProjectID);
            var returnValue = new List<BinCardViewModel>();
            decimal balance = 0;
            foreach(var res in results)
            {
                balance += (res.Received.HasValue) ? res.Received.Value : 0;
                balance -= (res.Dispatched.HasValue) ? res.Dispatched.Value : 0;

                returnValue.Add(new BinCardViewModel
                                    {
                                        SINumber = res.SINumber,
                                        DriverName = res.DriverName,
                                        Transporter = res.Transporter,
                                        TransporterAM = res.TransporterAM,
                                        Date = res.Date,
                                        Project = res.Projesct,
                                        Dispatched = res.Dispatched,
                                        Received = res.Received,
                                        Balance = balance,
                                        Identification = res.Identification,
                                        ToFrom = res.ToFrom

                                    });
            }

            return returnValue;
        }

        public bool DeleteByID(int id)
        {
            var origin = FindById(id);
            if(origin==null) return false;
            db.Stores.Remove(origin);
            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Store FindById(int id)
        {
            return db.Stores.FirstOrDefault(t => t.StoreID == id);
        }

        public Store FindById(System.Guid id)
        {
            return null;
        }
    }
}