using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class ForgetPasswordRequest
    {
        public int ForgetPasswordRequestID { get; set; }
        public int UserProfileID { get; set; }
        public System.DateTime GeneratedDate { get; set; }
        public System.DateTime ExpieryDate { get; set; }
        public bool Completed { get; set; }
        public string RequestKey { get; set; }
    }
}
