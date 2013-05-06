using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class HubRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public HubRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Hubs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Hub entity)
            {
                db.AddToHubs(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Hub entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Hubs.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Hub entity)
            {

                Hub original = db.Hubs.SingleOrDefault(p => p.HubID == entity.HubID);
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
				                Hub original = db.Hubs.SingleOrDefault(p => p.HubID == id);
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

            public List<Hub> GetAll()
            {
                return db.Hubs.ToList();
            }

            public Hub FindById(int id)
            {
				                		return db.Hubs.ToList().SingleOrDefault(p => p.HubID == id);
				            }
			
			public Hub FindById(Guid id)
            {
										return null;
				            }
    }
}
