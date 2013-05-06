using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DRMFSS.Web
{
    public class MembershipWrapper : IMembershipWrapper
    {
        public bool ValidateUser(string userName, string password)
        {
            //new DRMFSS.Web.MembershipProvider()
            return Membership.ValidateUser(userName, password);
        }
    }

    public interface IMembershipWrapper
    {
        bool ValidateUser(string userName, string password);
    }
}