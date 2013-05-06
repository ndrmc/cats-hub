using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class DispatchAllocationRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public DispatchAllocationRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.DispatchAllocations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(DispatchAllocation entity)
            {
                db.AddToDispatchAllocations(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(DispatchAllocation entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.DispatchAllocations.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(DispatchAllocation entity)
            {

                DispatchAllocation original = db.DispatchAllocations.SingleOrDefault(p => p.DispatchAllocationID == entity.DispatchAllocationID);
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
				                	DispatchAllocation original = db.DispatchAllocations.SingleOrDefault(p => p.DispatchAllocationID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<DispatchAllocation> GetAll()
            {
                return db.DispatchAllocations.ToList();
            }

            public DispatchAllocation FindById(int id)
            {
										return null;
				            }
			
			public DispatchAllocation FindById(Guid id)
            {
				                		return db.DispatchAllocations.ToList().SingleOrDefault(p => p.DispatchAllocationID == id);
				            }
    }
}
