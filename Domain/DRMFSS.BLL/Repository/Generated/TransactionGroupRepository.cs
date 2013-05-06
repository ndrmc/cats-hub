using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class TransactionGroupRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public TransactionGroupRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.TransactionGroups.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(TransactionGroup entity)
            {
                db.AddToTransactionGroups(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(TransactionGroup entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.TransactionGroups.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(TransactionGroup entity)
            {

                TransactionGroup original = db.TransactionGroups.SingleOrDefault(p => p.TransactionGroupID == entity.TransactionGroupID);
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
				                	TransactionGroup original = db.TransactionGroups.SingleOrDefault(p => p.TransactionGroupID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<TransactionGroup> GetAll()
            {
                return db.TransactionGroups.ToList();
            }

            public TransactionGroup FindById(int id)
            {
										return null;
				            }
			
			public TransactionGroup FindById(Guid id)
            {
				                		return db.TransactionGroups.ToList().SingleOrDefault(p => p.TransactionGroupID == id);
				            }
    }
}
