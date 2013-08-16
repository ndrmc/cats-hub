using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{
    public partial class Hub
    {
         [NotMapped]
        public string HubNameWithOwner {
            get { return this.Name + " : " + this.HubOwner.Name; } 
        }
    }
}
