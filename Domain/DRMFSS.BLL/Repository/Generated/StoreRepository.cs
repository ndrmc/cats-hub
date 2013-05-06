using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class StoreRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public StoreRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Stores.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Store entity)
            {
                db.AddToStores(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Store entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Stores.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Store entity)
            {

                Store original = db.Stores.SingleOrDefault(p => p.StoreID == entity.StoreID);
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
				                Store original = db.Stores.SingleOrDefault(p => p.StoreID == id);
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

            public List<Store> GetAll()
            {
                return db.Stores.ToList();
            }

            public Store FindById(int id)
            {
				                		return db.Stores.ToList().SingleOrDefault(p => p.StoreID == id);
				            }
			
			public Store FindById(Guid id)
            {
										return null;
				            }
    }
}
