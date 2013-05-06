using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AdjustmentRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AdjustmentRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Adjustments.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Adjustment entity)
            {
                db.AddToAdjustments(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Adjustment entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Adjustments.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Adjustment entity)
            {

                Adjustment original = db.Adjustments.SingleOrDefault(p => p.AdjustmentID == entity.AdjustmentID);
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
				                	Adjustment original = db.Adjustments.SingleOrDefault(p => p.AdjustmentID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<Adjustment> GetAll()
            {
                return db.Adjustments.ToList();
            }

            public Adjustment FindById(int id)
            {
										return null;
				            }
			
			public Adjustment FindById(Guid id)
            {
				                		return db.Adjustments.ToList().SingleOrDefault(p => p.AdjustmentID == id);
				            }
    }
}
