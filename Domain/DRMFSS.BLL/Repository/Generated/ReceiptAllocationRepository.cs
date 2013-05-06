using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ReceiptAllocationRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ReceiptAllocationRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ReceiptAllocations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ReceiptAllocation entity)
            {
                db.AddToReceiptAllocations(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ReceiptAllocation entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ReceiptAllocations.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ReceiptAllocation entity)
            {

                ReceiptAllocation original = db.ReceiptAllocations.SingleOrDefault(p => p.ReceiptAllocationID == entity.ReceiptAllocationID);
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
				                	ReceiptAllocation original = db.ReceiptAllocations.SingleOrDefault(p => p.ReceiptAllocationID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<ReceiptAllocation> GetAll()
            {
                return db.ReceiptAllocations.ToList();
            }

            public ReceiptAllocation FindById(int id)
            {
										return null;
				            }
			
			public ReceiptAllocation FindById(Guid id)
            {
				                		return db.ReceiptAllocations.ToList().SingleOrDefault(p => p.ReceiptAllocationID == id);
				            }
    }
}
