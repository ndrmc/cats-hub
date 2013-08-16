using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// Manges Admin Unit types
    /// </summary>
    public partial class AdminUnitTypeRepository :GenericRepository<CTSContext,AdminUnitType> ,IAdminUnitTypeRepository
    {
        public AdminUnitTypeRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.AdminUnitTypes.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public AdminUnitType FindById(int id)
        {
            return db.AdminUnitTypes.FirstOrDefault(t => t.AdminUnitTypeID == id);
        }

        public AdminUnitType FindById(System.Guid id)
        {
            return null;

        }
    }
}
