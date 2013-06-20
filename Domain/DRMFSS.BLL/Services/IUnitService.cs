using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public interface IUnitService
    {
       bool DeleteByID(int unitId);
       Unit FindById(int unitId);
    }
}
