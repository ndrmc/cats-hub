using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IUserRoleService
    {
        bool DeleteByID(int id);
        UserRole FindById(int id);
        

    }
}
