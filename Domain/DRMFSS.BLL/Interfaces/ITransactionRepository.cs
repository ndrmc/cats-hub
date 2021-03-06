using System.Collections.Generic;
using System;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL.ViewModels.Report;

namespace DRMFSS.BLL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionRepository: IGenericRepository<Transaction>,IRepository<Transaction>
    {
        /// <summary>
        /// Gets the active accounts for ledger.
        /// </summary>
        /// <param name="LedgerID">The ledger ID.</param>
        /// <returns></returns>
        List<Account> GetActiveAccountsForLedger(int LedgerID);
        /// <summary>
        /// Gets the transactions for ledger.
        /// </summary>
        /// <param name="LedgerID">The ledger ID.</param>
        /// <param name="AccountID">The account ID.</param>
        /// <param name="Commodity">The commodity.</param>
        /// <returns></returns>
        List<Transaction> GetTransactionsForLedger(int LedgerID, int AccountID, int Commodity);

        /// <summary>
        /// Gets the total receipt allocation.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        decimal GetTotalReceiptAllocation(int siNumber, int commodityId ,int hubId);

        /// <summary>
        /// Gets the total received from receipt allocation.
        /// </summary>
        /// <param name="siNumber">The SI number.</param>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        decimal GetTotalReceivedFromReceiptAllocation(int siNumber,int commodityId, int hubId);
        decimal GetTotalReceivedFromReceiptAllocation(string siNumber, int commodityId, int hubId);
        /// <summary>
        /// Saves the receipt transaction.
        /// </summary>
        /// <param name="receiveModels">The receive models.</param>
        /// <param name="user">The user.</param>
        void SaveReceiptTransaction(Web.Models.ReceiveViewModel receiveModels, UserProfile user);

        /// <summary>
        /// Saves the dispatch transaction.
        /// </summary>
        /// <param name="dispatchModel">The dispatch model.</param>
        /// <param name="user">The user.</param>
        void SaveDispatchTransaction(Web.Models.DispatchModel dispatchModel, UserProfile user);


        /// <summary>
        /// Gets the grouped transportation reports.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        List<ViewModels.GroupedTransportation> GetGroupedTransportationReports(ViewModels.OperationMode mode, DateTime? fromDate, DateTime? toDate);
        
        /// <summary>
        /// Gets the transportation reports.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        List<ViewModels.TransporationReport> GetTransportationReports(ViewModels.OperationMode mode, DateTime? fromDate, DateTime? toDate);
        
        /// <summary>
        /// Gets the available allocations.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        List<BLL.ReceiptAllocation> GetAvailableAllocations(int hubId);

        /// <summary>
        /// Finds the by transaction group ID.
        /// </summary>
        /// <param name="partition">The partition.</param>
        /// <param name="transactionGroupID">The transaction group ID.</param>
        /// <returns></returns>
        Transaction FindByTransactionGroupID( Guid transactionGroupID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        void SaveInternalMovementTrasnsaction(InternalMovementViewModel viewModel, UserProfile user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="user"></param>
        void SaveLossTrasnsaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="user"></param>
        void SaveAdjustmentTrasnsaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user);

        /// <summary>
        /// Saves the loss adjustment transaction.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="user">The user.</param>
        void SaveLossAdjustmentTransaction(LossesAndAdjustmentsViewModel viewModel, UserProfile user);

        /// <summary>
        /// Gets the commodity balance for store.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="si">The SI.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        decimal GetCommodityBalanceForStore(int storeId, int parentCommodityId, int si, int project);


        decimal GetCommodityBalanceForHub(int HubId, int parentCommodityId, int si, int project);

        /// <summary>
        /// Gets the commodity balance for stack.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="stack">The stack.</param>
        /// <param name="parentCommodityId">The parent commodity id.</param>
        /// <param name="si">The SI.</param>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        decimal GetCommodityBalanceForStack(int storeId, int stack, int parentCommodityId, int si, int project);

        void SaveStartingBalanceTransaction(StartingBalanceViewModel startingBalance, UserProfile user);

        List<StartingBalanceViewModelDto> GetListOfStartingBalances(int hubID);

        List<ViewModels.Report.Data.OffloadingReport> GetOffloadingReport(int hubID, DispatchesViewModel dispatchesViewModel);

        List<ViewModels.Report.Data.ReceiveReport> GetReceiveReport(int hubID, ReceiptsViewModel receiptsViewModel);

        List<ViewModels.Report.Data.DistributionRows> GetDistributionReport(int hubID, DistributionViewModel distributionViewModel);
    }
}
