using System;
using System.Linq;
using DRMFSS.BLL.Interfaces;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HubSettingValueRepository : GenericRepository<CTSContext, HubSettingValue>, IHubSettingValueRepository
    {
        public HubSettingValueRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }


        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.HubSettingValues.Remove(original);
            this.db.SaveChanges();
            return true;

        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public HubSettingValue FindById(Guid id)
        {
            return null;

        }

        public HubSettingValue FindById(int id)
        {
            return db.HubSettingValues.SingleOrDefault(p => p.HubSettingValueID == id);


        }

    }
}
