using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class HubSettingValueRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public HubSettingValueRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.HubSettingValues.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(HubSettingValue entity)
            {
                db.AddToHubSettingValues(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(HubSettingValue entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.HubSettingValues.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(HubSettingValue entity)
            {

                HubSettingValue original = db.HubSettingValues.SingleOrDefault(p => p.HubSettingValueID == entity.HubSettingValueID);
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
				                HubSettingValue original = db.HubSettingValues.SingleOrDefault(p => p.HubSettingValueID == id);
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

            public List<HubSettingValue> GetAll()
            {
                return db.HubSettingValues.ToList();
            }

            public HubSettingValue FindById(int id)
            {
				                		return db.HubSettingValues.ToList().SingleOrDefault(p => p.HubSettingValueID == id);
				            }
			
			public HubSettingValue FindById(Guid id)
            {
										return null;
				            }
    }
}
