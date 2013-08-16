using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITransporterRepository : IGenericRepository<Transporter>, IRepository<Transporter>
    {

        bool IsNameValid(int? TransporterID, string Name);
    }
}
