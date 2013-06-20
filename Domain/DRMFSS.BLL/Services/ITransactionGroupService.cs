using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ITransactionGroupService
    {
        Guid GetLastTrasactionGroupId();
        bool DeleteByID(int id);
        TransactionGroup FindById(System.Guid id);

    }
}
