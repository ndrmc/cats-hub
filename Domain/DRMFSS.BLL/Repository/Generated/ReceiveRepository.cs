using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ReceiveRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ReceiveRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Receives.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Receive entity)
            {
                db.AddToReceives(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Receive entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Receives.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Receive entity)
            {

                Receive original = db.Receives.SingleOrDefault(p => p.ReceiveID == entity.ReceiveID);
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
				                	Receive original = db.Receives.SingleOrDefault(p => p.ReceiveID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<Receive> GetAll()
            {
                return db.Receives.ToList();
            }

            public Receive FindById(int id)
            {
										return null;
				            }
			
			public Receive FindById(Guid id)
            {
				                		return db.Receives.ToList().SingleOrDefault(p => p.ReceiveID == id);
				            }
    }
}
