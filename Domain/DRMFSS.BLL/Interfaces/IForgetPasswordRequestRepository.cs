using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Interfaces
{
    public interface IForgetPasswordRequestRepository : IRepository<ForgetPasswordRequest>
    {
        /// <summary>
        /// Gets the valid request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        BLL.ForgetPasswordRequest GetValidRequest(string key);
        /// <summary>
        /// Invalidates the request.
        /// </summary>
        /// <param name="reqId">The req id.</param>
        void InvalidateRequest(int reqId);
    }
}
