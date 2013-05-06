using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class SettingRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public SettingRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Settings.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Setting entity)
            {
                db.AddToSettings(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Setting entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Settings.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Setting entity)
            {

                Setting original = db.Settings.SingleOrDefault(p => p.SettingID == entity.SettingID);
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
				                Setting original = db.Settings.SingleOrDefault(p => p.SettingID == id);
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

            public List<Setting> GetAll()
            {
                return db.Settings.ToList();
            }

            public Setting FindById(int id)
            {
				                		return db.Settings.ToList().SingleOrDefault(p => p.SettingID == id);
				            }
			
			public Setting FindById(Guid id)
            {
										return null;
				            }
    }
}
