using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    public partial class ForgetPasswordRequestRepository : IForgetPasswordRequestRepository
    {
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
    }
}
