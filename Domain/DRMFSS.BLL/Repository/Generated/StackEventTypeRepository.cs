using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class StackEventTypeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public StackEventTypeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.StackEventTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(StackEventType entity)
            {
                db.AddToStackEventTypes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(StackEventType entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.StackEventTypes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(StackEventType entity)
            {

                StackEventType original = db.StackEventTypes.SingleOrDefault(p => p.StackEventTypeID == entity.StackEventTypeID);
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
				                StackEventType original = db.StackEventTypes.SingleOrDefault(p => p.StackEventTypeID == id);
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

            public List<StackEventType> GetAll()
            {
                return db.StackEventTypes.ToList();
            }

            public StackEventType FindById(int id)
            {
				                		return db.StackEventTypes.ToList().SingleOrDefault(p => p.StackEventTypeID == id);
				            }
			
			public StackEventType FindById(Guid id)
            {
										return null;
				            }
    }
}
