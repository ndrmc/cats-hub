using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ISMSService
    {
        bool DeleteByID(int id);
        SMS FindById(int id);

    }
}
