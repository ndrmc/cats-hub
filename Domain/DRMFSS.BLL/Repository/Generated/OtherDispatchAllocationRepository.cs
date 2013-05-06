using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class OtherDispatchAllocationRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public OtherDispatchAllocationRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.OtherDispatchAllocations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(OtherDispatchAllocation entity)
            {
                db.AddToOtherDispatchAllocations(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(OtherDispatchAllocation entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.OtherDispatchAllocations.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(OtherDispatchAllocation entity)
            {

                OtherDispatchAllocation original = db.OtherDispatchAllocations.SingleOrDefault(p => p.OtherDispatchAllocationID == entity.OtherDispatchAllocationID);
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
				                	OtherDispatchAllocation original = db.OtherDispatchAllocations.SingleOrDefault(p => p.OtherDispatchAllocationID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<OtherDispatchAllocation> GetAll()
            {
                return db.OtherDispatchAllocations.ToList();
            }

            public OtherDispatchAllocation FindById(int id)
            {
										return null;
				            }
			
			public OtherDispatchAllocation FindById(Guid id)
            {
				                		return db.OtherDispatchAllocations.ToList().SingleOrDefault(p => p.OtherDispatchAllocationID == id);
				            }
    }
}
