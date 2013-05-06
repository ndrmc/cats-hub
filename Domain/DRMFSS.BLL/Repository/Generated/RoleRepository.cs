using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class RoleRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public RoleRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Roles.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Role entity)
            {
                db.AddToRoles(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Role entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Roles.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Role entity)
            {

                Role original = db.Roles.SingleOrDefault(p => p.RoleID == entity.RoleID);
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
				                Role original = db.Roles.SingleOrDefault(p => p.RoleID == id);
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

            public List<Role> GetAll()
            {
                return db.Roles.ToList();
            }

            public Role FindById(int id)
            {
				                		return db.Roles.ToList().SingleOrDefault(p => p.RoleID == id);
				            }
			
			public Role FindById(Guid id)
            {
										return null;
				            }
    }
}
