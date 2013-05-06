using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class SessionAttemptRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public SessionAttemptRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.SessionAttempts.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(SessionAttempt entity)
            {
                db.AddToSessionAttempts(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(SessionAttempt entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.SessionAttempts.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(SessionAttempt entity)
            {

                SessionAttempt original = db.SessionAttempts.SingleOrDefault(p => p.SessionAttemptID == entity.SessionAttemptID);
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
				                	SessionAttempt original = db.SessionAttempts.SingleOrDefault(p => p.SessionAttemptID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<SessionAttempt> GetAll()
            {
                return db.SessionAttempts.ToList();
            }

            public SessionAttempt FindById(int id)
            {
										return null;
				            }
			
			public SessionAttempt FindById(Guid id)
            {
				                		return db.SessionAttempts.ToList().SingleOrDefault(p => p.SessionAttemptID == id);
				            }
    }
}
