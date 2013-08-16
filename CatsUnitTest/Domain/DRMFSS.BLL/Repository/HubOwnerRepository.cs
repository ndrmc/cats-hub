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
    public partial class HubOwnerRepository :GenericRepository<CTSContext,HubOwner>, IHubOwnerRepository
    {
        public HubOwnerRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.HubOwners.Remove(original);
            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public HubOwner FindById(int id)
        {
            return db.HubOwners.FirstOrDefault(t => t.HubOwnerID == id);
        }

        public HubOwner FindById(System.Guid id)
        {
            return null;
        }
    }
}
