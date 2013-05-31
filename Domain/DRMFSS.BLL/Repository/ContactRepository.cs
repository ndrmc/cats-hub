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
    public partial class ContactRepository : GenericRepository<CTSContext,Contact>,IContactRepository
    {
        public ContactRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
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

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Contacts.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Contact FindById(int id)
        {
            return db.Contacts.FirstOrDefault(t => t.ContactID == id);
        }

        public Contact FindById(System.Guid id)
        {
            return null;
        }
    }
}
