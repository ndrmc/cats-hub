using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class TransactionRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public TransactionRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Transactions.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Transaction entity)
            {
                db.AddToTransactions(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Transaction entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Transactions.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Transaction entity)
            {

                Transaction original = db.Transactions.SingleOrDefault(p => p.TransactionID == entity.TransactionID);
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
										return false;
					            }

			public bool DeleteByID(Guid id)
            {
				                	Transaction original = db.Transactions.SingleOrDefault(p => p.TransactionID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<Transaction> GetAll()
            {
                return db.Transactions.ToList();
            }

            public Transaction FindById(int id)
            {
										return null;
				            }
			
			public Transaction FindById(Guid id)
            {
				                		return db.Transactions.ToList().SingleOrDefault(p => p.TransactionID == id);
				            }
    }
}
