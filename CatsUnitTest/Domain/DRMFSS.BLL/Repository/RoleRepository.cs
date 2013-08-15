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
    public partial class RoleRepository :GenericRepository<CTSContext,Role>, IRoleRepository
    {
        public RoleRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Roles .Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Role FindById(int id)
        {
            return db.Roles.FirstOrDefault(t => t.RoleID == id);
        }

        public Role FindById(System.Guid id)
        {
            return null;
        }
    }
}
