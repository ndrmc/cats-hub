using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class SessionHistoryRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public SessionHistoryRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.SessionHistories.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(SessionHistory entity)
            {
                db.AddToSessionHistories(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(SessionHistory entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.SessionHistories.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(SessionHistory entity)
            {

                SessionHistory original = db.SessionHistories.SingleOrDefault(p => p.SessionHistoryID == entity.SessionHistoryID);
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
				                	SessionHistory original = db.SessionHistories.SingleOrDefault(p => p.SessionHistoryID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<SessionHistory> GetAll()
            {
                return db.SessionHistories.ToList();
            }

            public SessionHistory FindById(int id)
            {
										return null;
				            }
			
			public SessionHistory FindById(Guid id)
            {
				                		return db.SessionHistories.ToList().SingleOrDefault(p => p.SessionHistoryID == id);
				            }
    }
}
