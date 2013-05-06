using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.Web.Models;
using System;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IReceiveRepository : IRepository<Receive>
    {
        /// <summary>
        /// Bies the hub id.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        List<Receive> ByHubId(int hubId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<PortViewModel> GetALlPorts();

        Transaction GetReceiveTransaction(Guid receiveId);

        List<ReceiveViewModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid receiptAllocationId);
    }
}
