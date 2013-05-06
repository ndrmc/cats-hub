using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class UnitRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public UnitRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Units.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Unit entity)
            {
                db.AddToUnits(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Unit entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Units.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Unit entity)
            {

                Unit original = db.Units.SingleOrDefault(p => p.UnitID == entity.UnitID);
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
				                Unit original = db.Units.SingleOrDefault(p => p.UnitID == id);
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

            public List<Unit> GetAll()
            {
                return db.Units.ToList();
            }

            public Unit FindById(int id)
            {
				                		return db.Units.ToList().SingleOrDefault(p => p.UnitID == id);
				            }
			
			public Unit FindById(Guid id)
            {
										return null;
				            }
    }
}
