using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class HubOwnerRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public HubOwnerRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.HubOwners.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(HubOwner entity)
            {
                db.AddToHubOwners(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(HubOwner entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.HubOwners.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(HubOwner entity)
            {

                HubOwner original = db.HubOwners.SingleOrDefault(p => p.HubOwnerID == entity.HubOwnerID);
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
				                HubOwner original = db.HubOwners.SingleOrDefault(p => p.HubOwnerID == id);
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

            public List<HubOwner> GetAll()
            {
                return db.HubOwners.ToList();
            }

            public HubOwner FindById(int id)
            {
				                		return db.HubOwners.ToList().SingleOrDefault(p => p.HubOwnerID == id);
				            }
			
			public HubOwner FindById(Guid id)
            {
										return null;
				            }
    }
}
