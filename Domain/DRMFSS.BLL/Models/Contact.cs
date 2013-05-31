using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Contact
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public int FDPID { get; set; }
        public virtual FDP FDP { get; set; }
    }
}
