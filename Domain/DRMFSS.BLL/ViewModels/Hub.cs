using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{
    public partial class Hub
    {
        public string HubNameWithOwner {
            get { return this.Name + " : " + this.HubOwner.Name; } 
        }
    }
}
