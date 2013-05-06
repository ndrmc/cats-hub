using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.Web.Models;
using System;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IReceiveDetailRepository : IRepository<ReceiveDetail>
    {
        /// <summary>
        /// Gets the by receive id.
        /// </summary>
        /// <param name="p">The receive id.</param>
        /// <returns></returns>
        List<ReceiveDetail> GetByReceiveId(Guid receiveId);

        List<ReceiveDetailViewModelDto> ByReceiveIDetached(Guid? receiveId,string weightMeasurmentCode);
    }
}
