using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class StackEventRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public StackEventRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.StackEvents.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(StackEvent entity)
            {
                db.AddToStackEvents(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(StackEvent entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.StackEvents.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(StackEvent entity)
            {

                StackEvent original = db.StackEvents.SingleOrDefault(p => p.StackEventID == entity.StackEventID);
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
				                	StackEvent original = db.StackEvents.SingleOrDefault(p => p.StackEventID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<StackEvent> GetAll()
            {
                return db.StackEvents.ToList();
            }

            public StackEvent FindById(int id)
            {
										return null;
				            }
			
			public StackEvent FindById(Guid id)
            {
				                		return db.StackEvents.ToList().SingleOrDefault(p => p.StackEventID == id);
				            }
    }
}
