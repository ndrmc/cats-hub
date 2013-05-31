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
    public partial class SettingRepository :GenericRepository<CTSContext,Setting>, ISettingRepository
    {
        public SettingRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <returns></returns>
        public string GetSettingValue(string Key)
        {
            string settingValue = (from v in db.Settings
                                   where v.Key == Key
                                   select v.Value).FirstOrDefault();
            return settingValue;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Settings.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Setting FindById(int id)
        {
            return db.Settings.FirstOrDefault(t => t.SettingID == id);
        }

        public Setting FindById(System.Guid id)
        {
            return null;
        }
    }
}
