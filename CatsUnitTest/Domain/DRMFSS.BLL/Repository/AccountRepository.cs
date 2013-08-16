using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;


namespace DRMFSS.BLL.Repository
{
    public class AccountRepository :
          GenericRepository<CTSContext, Account>, IAccountRepository
    {
        public AccountRepository(CTSContext _db,IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public bool DeleteByID(int id)
        {
            Account original =this.db.Accounts.SingleOrDefault(p => p.AccountID == id);
            if (original != null)
            {
                this.db.Accounts.Remove(original);
                this.db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteByID(Guid id)
        {
            return false;
        }

        public Account FindById(Guid id)
        {
            return null;
        }

        public Account FindById(int id)
        {
            return db.Accounts.ToList().SingleOrDefault(p => p.AccountID == id);
				
        }


        /// <summary>
        /// Gets the account ID.
        /// </summary>
        /// <param name="EntityType">Type of the entity.</param>
        /// <param name="EntityID">The entity ID.</param>
        /// <returns></returns>
        public int GetAccountID(string EntityType, int EntityID)
        {
            var account = (from v in db.Accounts
                           where v.EntityType == EntityType && v.EntityID == EntityID
                           select v).FirstOrDefault();
            if (account != null)
            {
                return account.AccountID;
            }
            // -1 represents an account was not found for this entity
            return -1;
        }

        /// <summary>
        /// Gets the account ID with create.
        /// </summary>
        /// <param name="EntityType">Type of the entity.</param>
        /// <param name="EntityID">The entity ID.</param>
        /// <returns></returns>
        public int GetAccountIDWithCreate(string EntityType, int EntityID)
        {
            var account = from v in db.Accounts
                          where v.EntityType == EntityType && v.EntityID == EntityID
                          select v;
            if (account.Count() == 0)
            {
                // this means it doesn't exist, insert it here.
                Account acc = new Account();
                acc.EntityID = EntityID;
                acc.EntityType = EntityType;
                db.Accounts.Add(acc);
                db.SaveChanges();
                return acc.AccountID;
            }

            return account.FirstOrDefault().AccountID;
        }

    }
}
