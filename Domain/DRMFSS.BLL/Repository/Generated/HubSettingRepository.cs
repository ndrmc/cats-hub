using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class HubSettingRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public HubSettingRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.HubSettings.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(HubSetting entity)
            {
                db.AddToHubSettings(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(HubSetting entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.HubSettings.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(HubSetting entity)
            {

                HubSetting original = db.HubSettings.SingleOrDefault(p => p.HubSettingID == entity.HubSettingID);
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
				                HubSetting original = db.HubSettings.SingleOrDefault(p => p.HubSettingID == id);
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

            public List<HubSetting> GetAll()
            {
                return db.HubSettings.ToList();
            }

            public HubSetting FindById(int id)
            {
				                		return db.HubSettings.ToList().SingleOrDefault(p => p.HubSettingID == id);
				            }
			
			public HubSetting FindById(Guid id)
            {
										return null;
				            }
    }
}
