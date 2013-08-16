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
    public partial class UnitRepository :GenericRepository<CTSContext,Unit> ,IUnitRepository
    {
        public UnitRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Units.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Unit FindById(int id)
        {
            return db.Units.FirstOrDefault(t => t.UnitID == id);
        }

        public Unit  FindById(System.Guid id)
        {
            return null;
        }
    }
}
