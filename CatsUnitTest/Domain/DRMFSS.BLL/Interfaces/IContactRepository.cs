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
    public interface IContactRepository : IGenericRepository<Contact>,IRepository<Contact>
    {

        /// <summary>
        /// Gets the List Of contacts by FDP.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        List<Contact> GetByFdp(int id);
    }
}
