using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TransactionGroupRepository:Interfaces.ITransactionGroupRepository
    {

        public Guid GetLastTrasactionGroupId()
        {
            Guid trasactionGroupId = db.TransactionGroups.OrderByDescending(c => c.TransactionGroupID).Select(x => x.TransactionGroupID).First();
            return trasactionGroupId;
        }
    }
}
