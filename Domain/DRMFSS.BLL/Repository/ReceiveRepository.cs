using System;
using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.Web.Models;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReceiveRepository : IReceiveRepository
    {

        /// <summary>
        /// Bies the hub id.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<Receive> ByHubId(int hubId)
        {
            return db.Receives.Where(p => p.HubID == hubId).ToList();
        }
        /// <summary>
        /// Return All Ports
        /// </summary>
        /// <returns></returns>
        public List<PortViewModel> GetALlPorts()
        {
            var ports = (from c in db.Receives select new PortViewModel() { PortName = c.PortName }).Distinct().ToList();

            ports.Insert(0, new PortViewModel { PortName = "All Ports"});
            return ports;
        }

        public Transaction GetReceiveTransaction(Guid receiveId)
        {
            var transactionGroup = (from d in db.ReceiveDetails
                                    where d.ReceiveID == receiveId
                                    select d.TransactionGroup).FirstOrDefault();
            if (transactionGroup != null && transactionGroup.Transactions.Count > 0)
            {
                return transactionGroup.Transactions.First();
            }
            return null;
        }


        public List<ReceiveViewModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid receiptAllocationId)
        {
            List<ReceiveViewModelDto> receives = new List<ReceiveViewModelDto>();

            var query = (from r in db.Receives
                         where r.HubID == hubId && r.ReceiptAllocationID == receiptAllocationId
                         select new ReceiveViewModelDto()
                                    {
                                        ReceiptDate = r.ReceiptDate,
                                        GRN = r.GRN,
                                        ReceivedByStoreMan = r.ReceivedByStoreMan,
                                        ReceiveID = r.ReceiveID
                                    });

            return query.ToList();
        }
    }
}
