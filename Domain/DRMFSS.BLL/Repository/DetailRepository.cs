using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Common;


namespace DRMFSS.BLL.Repository
{
    public partial class DetailRepository : IDetailRepository
    {

        /// <summary>
        /// Gets the queriable list of details by master ID.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
        public IQueryable<Detail> GetByMasterID(int masterId)
        {
            return (from v in db.Details
                    where v.MasterID == masterId
                    select v).OrderBy(o => o.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public List<ViewModels.Common.ReasonViewModel> GetReasonByMaster(int masterId)
        {
            var reasons = (from c in db.Details where c.MasterID == masterId select new ReasonViewModel { ReasonId = c.DetailID, ReasonName = c.Name }).ToList();
            return reasons;
        }
    }
}
