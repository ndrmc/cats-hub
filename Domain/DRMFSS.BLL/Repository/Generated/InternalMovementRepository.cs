using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class InternalMovementRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public InternalMovementRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.InternalMovements.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(InternalMovement entity)
            {
                db.AddToInternalMovements(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(InternalMovement entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.InternalMovements.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(InternalMovement entity)
            {

                InternalMovement original = db.InternalMovements.SingleOrDefault(p => p.InternalMovementID == entity.InternalMovementID);
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
				                	InternalMovement original = db.InternalMovements.SingleOrDefault(p => p.InternalMovementID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<InternalMovement> GetAll()
            {
                return db.InternalMovements.ToList();
            }

            public InternalMovement FindById(int id)
            {
										return null;
				            }
			
			public InternalMovement FindById(Guid id)
            {
				                		return db.InternalMovements.ToList().SingleOrDefault(p => p.InternalMovementID == id);
				            }
    }
}
