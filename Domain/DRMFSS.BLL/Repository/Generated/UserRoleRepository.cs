using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class UserRoleRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public UserRoleRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.UserRoles.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(UserRole entity)
            {
                db.AddToUserRoles(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(UserRole entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.UserRoles.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(UserRole entity)
            {

                UserRole original = db.UserRoles.SingleOrDefault(p => p.UserRoleID == entity.UserRoleID);
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
				                UserRole original = db.UserRoles.SingleOrDefault(p => p.UserRoleID == id);
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

            public List<UserRole> GetAll()
            {
                return db.UserRoles.ToList();
            }

            public UserRole FindById(int id)
            {
				                		return db.UserRoles.ToList().SingleOrDefault(p => p.UserRoleID == id);
				            }
			
			public UserRole FindById(Guid id)
            {
										return null;
				            }
    }
}
