using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AccountRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AccountRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Accounts.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Account entity)
            {
                db.AddToAccounts(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Account entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Accounts.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Account entity)
            {

                Account original = db.Accounts.SingleOrDefault(p => p.AccountID == entity.AccountID);
                if (original != null)
                {
                    db.DeleteObject(original);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

            public bool DeleteByID(int id)
            {
				                Account original = db.Accounts.SingleOrDefault(p => p.AccountID == id);
                if (original != null)
                {
                    db.DeleteObject(original);
                    db.SaveChanges();
                    return true;
                }
                return false;
				            }

			public bool DeleteByID(Guid id)
            {
										return false;
					            }

            public List<Account> GetAll()
            {
                return db.Accounts.ToList();
            }

            public Account FindById(int id)
            {
				                		return db.Accounts.ToList().SingleOrDefault(p => p.AccountID == id);
				            }
			
			public Account FindById(Guid id)
            {
										return null;
				            }
    }
}
