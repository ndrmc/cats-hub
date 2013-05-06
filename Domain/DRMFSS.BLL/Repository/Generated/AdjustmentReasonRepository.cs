using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AdjustmentReasonRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AdjustmentReasonRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.AdjustmentReasons.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(AdjustmentReason entity)
            {
                db.AddToAdjustmentReasons(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(AdjustmentReason entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.AdjustmentReasons.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(AdjustmentReason entity)
            {

                AdjustmentReason original = db.AdjustmentReasons.SingleOrDefault(p => p.AdjustmentReasonID == entity.AdjustmentReasonID);
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
				                AdjustmentReason original = db.AdjustmentReasons.SingleOrDefault(p => p.AdjustmentReasonID == id);
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

            public List<AdjustmentReason> GetAll()
            {
                return db.AdjustmentReasons.ToList();
            }

            public AdjustmentReason FindById(int id)
            {
				                		return db.AdjustmentReasons.ToList().SingleOrDefault(p => p.AdjustmentReasonID == id);
				            }
			
			public AdjustmentReason FindById(Guid id)
            {
										return null;
				            }
    }
}
