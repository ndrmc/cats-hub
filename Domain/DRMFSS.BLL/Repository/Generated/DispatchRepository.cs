using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class DispatchRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public DispatchRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Dispatches.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Dispatch entity)
            {
                db.AddToDispatches(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Dispatch entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Dispatches.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Dispatch entity)
            {

                Dispatch original = db.Dispatches.SingleOrDefault(p => p.DispatchID == entity.DispatchID);
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
				                	Dispatch original = db.Dispatches.SingleOrDefault(p => p.DispatchID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<Dispatch> GetAll()
            {
                return db.Dispatches.ToList();
            }

            public Dispatch FindById(int id)
            {
										return null;
				            }
			
			public Dispatch FindById(Guid id)
            {
				                		return db.Dispatches.ToList().SingleOrDefault(p => p.DispatchID == id);
				            }
    }
}
