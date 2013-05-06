using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace DRMFSS.Web.Models
{
    public class UserHubsModel
    {
        public UserHubModel[] UserHubs { get; set; }
    }

    public class UserHubModel
    {
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public int SortOrder { get; set; }
    }
}