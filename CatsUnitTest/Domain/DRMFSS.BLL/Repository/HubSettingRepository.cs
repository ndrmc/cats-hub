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
    public partial class HubSettingRepository :GenericRepository<CTSContext,HubSetting>, IHubSettingRepository
    {
        public HubSettingRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.HubSettings.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public HubSetting FindById(int id)
        {
            return db.HubSettings.FirstOrDefault(t => t.HubSettingID == id);
        }

        public HubSetting FindById(System.Guid id)
        {
            return null;
        }
    }
}
