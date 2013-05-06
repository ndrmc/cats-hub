using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class FDPRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public FDPRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.FDPs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(FDP entity)
            {
                db.AddToFDPs(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(FDP entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.FDPs.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(FDP entity)
            {

                FDP original = db.FDPs.SingleOrDefault(p => p.FDPID == entity.FDPID);
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
				                FDP original = db.FDPs.SingleOrDefault(p => p.FDPID == id);
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

            public List<FDP> GetAll()
            {
                return db.FDPs.ToList();
            }

            public FDP FindById(int id)
            {
				                		return db.FDPs.ToList().SingleOrDefault(p => p.FDPID == id);
				            }
			
			public FDP FindById(Guid id)
            {
										return null;
				            }
    }
}
