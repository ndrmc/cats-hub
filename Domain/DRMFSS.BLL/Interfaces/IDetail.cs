using System.Linq;
using DRMFSS.BLL.ViewModels.Common;
using System.Collections.Generic;

namespace DRMFSS.BLL.Interfaces
{

    public interface IDetailRepository : IRepository<Detail>
    {
        /// <summary>
        /// Gets the List of Details by master ID.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
         IQueryable<Detail> GetByMasterID(int masterId);
        List<ReasonViewModel> GetReasonByMaster(int masterId);

    }
}
