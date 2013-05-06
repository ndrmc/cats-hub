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
    public partial class ContactRepository : IContactRepository
    {

        /// <summary>
        /// Gets the list of contacts by FDP.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public List<Contact> GetByFdp(int id)
        {
            return (from v in db.Contacts
                    where v.FDPID == id
                    select v).ToList();
        }
    }
}
