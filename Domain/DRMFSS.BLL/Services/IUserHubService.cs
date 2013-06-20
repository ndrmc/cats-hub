using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IUserHubService
    {
        bool DeleteByID(int HubId);
        UserHub FindById(int HubId);
    }
}
