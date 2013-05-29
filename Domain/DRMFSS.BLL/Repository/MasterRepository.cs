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
    public partial class MasterRepository : GenericRepository<CTSContext, Master>, IMasterRepository
    {
        public MasterRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Masters.Remove(original); 
            db.SaveChanges();

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Master FindById(int id)
        {
            return db.Masters.FirstOrDefault(t => t.MasterID == id);
        }

        public Master FindById(System.Guid id)
        {
            return null;
        }
    }
}
