using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class PeriodRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public PeriodRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Periods.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Period entity)
            {
                db.AddToPeriods(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Period entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Periods.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Period entity)
            {

                Period original = db.Periods.SingleOrDefault(p => p.PeriodID == entity.PeriodID);
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
				                Period original = db.Periods.SingleOrDefault(p => p.PeriodID == id);
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

            public List<Period> GetAll()
            {
                return db.Periods.ToList();
            }

            public Period FindById(int id)
            {
				                		return db.Periods.ToList().SingleOrDefault(p => p.PeriodID == id);
				            }
			
			public Period FindById(Guid id)
            {
										return null;
				            }
    }
}
