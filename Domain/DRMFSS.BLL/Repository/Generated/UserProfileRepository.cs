using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class UserProfileRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public UserProfileRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.UserProfiles.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(UserProfile entity)
            {
                db.AddToUserProfiles(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(UserProfile entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.UserProfiles.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(UserProfile entity)
            {

                UserProfile original = db.UserProfiles.SingleOrDefault(p => p.UserProfileID == entity.UserProfileID);
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
				                UserProfile original = db.UserProfiles.SingleOrDefault(p => p.UserProfileID == id);
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

            public List<UserProfile> GetAll()
            {
                return db.UserProfiles.ToList();
            }

            public UserProfile FindById(int id)
            {
				                		return db.UserProfiles.ToList().SingleOrDefault(p => p.UserProfileID == id);
				            }
			
			public UserProfile FindById(Guid id)
            {
										return null;
				            }
    }
}
