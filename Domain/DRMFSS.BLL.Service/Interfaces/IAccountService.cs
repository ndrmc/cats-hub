using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public  interface IAccountService
   {
       
       bool AddAccount(Account entity);
       bool DeleteAccount(Account account);
       bool DeleteById(int id);
       bool EditAccount(Account account);
       Account FindById(int id);
       int GetAccountId(string entityType, int entityId);
       int GetAccountIdWithCreate(string entityType, int entityId);
       List<Account> GetAllAccount();
       List<Account> FindBy(Expression<Func<Account, bool>> predicate);


   }
}
