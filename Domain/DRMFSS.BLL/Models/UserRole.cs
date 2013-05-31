using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class UserRole
    {
        public int UserRoleID { get; set; }
        public int UserProfileID { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
