using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class DispatchDetailRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public DispatchDetailRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.DispatchDetails.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(DispatchDetail entity)
            {
                db.AddToDispatchDetails(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(DispatchDetail entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.DispatchDetails.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(DispatchDetail entity)
            {

                DispatchDetail original = db.DispatchDetails.SingleOrDefault(p => p.DispatchDetailID == entity.DispatchDetailID);
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
				                	DispatchDetail original = db.DispatchDetails.SingleOrDefault(p => p.DispatchDetailID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<DispatchDetail> GetAll()
            {
                return db.DispatchDetails.ToList();
            }

            public DispatchDetail FindById(int id)
            {
										return null;
				            }
			
			public DispatchDetail FindById(Guid id)
            {
				                		return db.DispatchDetails.ToList().SingleOrDefault(p => p.DispatchDetailID == id);
				            }
    }
}
