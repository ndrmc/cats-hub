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
    public partial class UserRoleRepository :GenericRepository<CTSContext,UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.UserRoles.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public UserRole FindById(int id)
        {
            return db.UserRoles.FirstOrDefault(t => t.UserRoleID == id);
        }

        public UserRole FindById(System.Guid id)
        {
            return null;
        }
    }
}
