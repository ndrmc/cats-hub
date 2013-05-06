using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class UserHubRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public UserHubRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.UserHubs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(UserHub entity)
            {
                db.AddToUserHubs(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(UserHub entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.UserHubs.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(UserHub entity)
            {

                UserHub original = db.UserHubs.SingleOrDefault(p => p.UserHubID == entity.UserHubID);
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
				                UserHub original = db.UserHubs.SingleOrDefault(p => p.UserHubID == id);
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

            public List<UserHub> GetAll()
            {
                return db.UserHubs.ToList();
            }

            public UserHub FindById(int id)
            {
				                		return db.UserHubs.ToList().SingleOrDefault(p => p.UserHubID == id);
				            }
			
			public UserHub FindById(Guid id)
            {
										return null;
				            }
    }
}
