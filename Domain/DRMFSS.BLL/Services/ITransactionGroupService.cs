using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ITransactionGroupService
    {

        bool AddTransactionGroup(TransactionGroup entity);
        bool DeleteTransactionGroup(TransactionGroup entity);
        bool DeleteById(int id);
        bool EditTransactionGroup(TransactionGroup entity);
        TransactionGroup FindById(int id);
        List<TransactionGroup> GetAllTransactionGroup();
        List<TransactionGroup> FindBy(Expression<Func<TransactionGroup, bool>> predicate);

        Guid GetLastTrasactionGroupId();
       

    }
}


      


      