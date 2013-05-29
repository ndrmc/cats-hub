using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class ForgetPasswordRequestRepository :GenericRepository<CTSContext,ForgetPasswordRequest>, IForgetPasswordRequestRepository
    {
        public ForgetPasswordRequestRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        /// <summary>
        /// Gets the valid password reset request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ForgetPasswordRequest GetValidRequest(string key)
        {
            return (from req in db.ForgetPasswordRequests
                    where req.RequestKey == key && req.ExpieryDate > DateTime.Now && !req.Completed
                    select req).SingleOrDefault();
        }

        /// <summary>
        /// Invalidates the request.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void InvalidateRequest(int userId)
        {
            var reqs = db.ForgetPasswordRequests.Where(p => p.UserProfileID == userId).ToList();
            if (reqs.Count > 0)
            {
                foreach (ForgetPasswordRequest req in reqs)
                {
                    req.Completed = true;
                }
                db.SaveChanges();
            }
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;

            this.db.ForgetPasswordRequests.Remove(original);
            this.db.SaveChanges();
            return true;

        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public ForgetPasswordRequest FindById(Guid id)
        {
              return null;

        }

        public ForgetPasswordRequest FindById(int id)
        {
            return db.ForgetPasswordRequests.SingleOrDefault(p => p.ForgetPasswordRequestID == id);
           

        }
    }
}
