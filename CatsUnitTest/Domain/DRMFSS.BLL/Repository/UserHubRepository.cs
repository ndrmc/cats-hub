using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{

    public class UserHubRepository :
          GenericRepository<CTSContext, UserHub>, IUserHubRepository  {
        public UserHubRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }


        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.UserHubs.Remove(original);
            return true;
        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public UserHub FindById(int id)
        {
            return db.UserHubs.FirstOrDefault(t => t.HubID == id);
        }

        public UserHub FindById(Guid id)
        {
            return null;
        }
          }
               
      
}
