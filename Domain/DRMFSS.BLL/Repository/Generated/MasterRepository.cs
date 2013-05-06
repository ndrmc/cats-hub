using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class MasterRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public MasterRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Masters.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Master entity)
            {
                db.AddToMasters(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Master entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Masters.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Master entity)
            {

                Master original = db.Masters.SingleOrDefault(p => p.MasterID == entity.MasterID);
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
				                Master original = db.Masters.SingleOrDefault(p => p.MasterID == id);
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

            public List<Master> GetAll()
            {
                return db.Masters.ToList();
            }

            public Master FindById(int id)
            {
				                		return db.Masters.ToList().SingleOrDefault(p => p.MasterID == id);
				            }
			
			public Master FindById(Guid id)
            {
										return null;
				            }
    }
}
