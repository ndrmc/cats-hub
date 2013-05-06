using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TransporterRepository : ITransporterRepository
    {
        public bool IsNameValid(int? TransporterID, string Name)
        {
            return !(from v in db.Transporters
                    where v.Name == Name && v.TransporterID != TransporterID
                    select v).Any();
        }
    }
}
