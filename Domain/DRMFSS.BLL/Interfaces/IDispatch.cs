using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using System;
using DRMFSS.BLL.ViewModels;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDispatchRepository : IRepository<Dispatch>
    {
        /// <summary>
        /// Gets the dispatch by GIN.
        /// </summary>
        /// <param name="ginNo">The gin no.</param>
        /// <returns></returns>
        Dispatch GetDispatchByGIN(string ginNo);
        /// <summary>
        /// Gets the dispatch transaction.
        /// </summary>
        /// <param name="dispatchId">The dispatch id.</param>
        /// <returns></returns>
        Transaction GetDispatchTransaction(Guid dispatchId);
        /// <summary>
        /// Gets the FDP balance.
        /// </summary>
        /// <param name="FDPID">The FDPID.</param>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        FDPBalance GetFDPBalance(int FDPID, string SINumber);
        /// <summary>
        /// Gets the available commodities.
        /// </summary>
        /// <param name="SINumber">The SI number.</param>
        /// <returns></returns>
        List<Commodity> GetAvailableCommodities(string SINumber, int hubID);


        List<Web.Models.DispatchModelModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid dispatchAllocationId);
        List<Web.Models.DispatchModelModelDto> ByHubIdAndOtherAllocationIDetached(int hubId, Guid otherDispatchAllocationId);
    }
}
