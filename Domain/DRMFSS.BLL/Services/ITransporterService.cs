using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ITransporterService
    {
        bool IsNameValid(int? TransporterID, string Name);
        bool DeleteByID(int TransporterId);
        Transporter FindById(int TransporterId);
    }
}
