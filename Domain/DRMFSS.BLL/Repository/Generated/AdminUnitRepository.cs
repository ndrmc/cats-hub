using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AdminUnitRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AdminUnitRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.AdminUnits.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(AdminUnit entity)
            {
                db.AddToAdminUnits(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(AdminUnit entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.AdminUnits.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(AdminUnit entity)
            {

                AdminUnit original = db.AdminUnits.SingleOrDefault(p => p.AdminUnitID == entity.AdminUnitID);
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
				                AdminUnit original = db.AdminUnits.SingleOrDefault(p => p.AdminUnitID == id);
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

            public List<AdminUnit> GetAll()
            {
                return db.AdminUnits.ToList();
            }

            public AdminUnit FindById(int id)
            {
				                		return db.AdminUnits.ToList().SingleOrDefault(p => p.AdminUnitID == id);
				            }
			
			public AdminUnit FindById(Guid id)
            {
										return null;
				            }
    }
}
