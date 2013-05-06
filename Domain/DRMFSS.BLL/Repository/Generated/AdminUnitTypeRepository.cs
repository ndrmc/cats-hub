using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AdminUnitTypeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AdminUnitTypeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.AdminUnitTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(AdminUnitType entity)
            {
                db.AddToAdminUnitTypes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(AdminUnitType entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.AdminUnitTypes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(AdminUnitType entity)
            {

                AdminUnitType original = db.AdminUnitTypes.SingleOrDefault(p => p.AdminUnitTypeID == entity.AdminUnitTypeID);
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
				                AdminUnitType original = db.AdminUnitTypes.SingleOrDefault(p => p.AdminUnitTypeID == id);
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

            public List<AdminUnitType> GetAll()
            {
                return db.AdminUnitTypes.ToList();
            }

            public AdminUnitType FindById(int id)
            {
				                		return db.AdminUnitTypes.ToList().SingleOrDefault(p => p.AdminUnitTypeID == id);
				            }
			
			public AdminUnitType FindById(Guid id)
            {
										return null;
				            }
    }
}
