using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class SMSRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public SMSRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.SMS.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(SMS entity)
            {
                db.AddToSMS(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(SMS entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.SMS.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(SMS entity)
            {

                SMS original = db.SMS.SingleOrDefault(p => p.SMSID == entity.SMSID);
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
				                SMS original = db.SMS.SingleOrDefault(p => p.SMSID == id);
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

            public List<SMS> GetAll()
            {
                return db.SMS.ToList();
            }

            public SMS FindById(int id)
            {
				                		return db.SMS.ToList().SingleOrDefault(p => p.SMSID == id);
				            }
			
			public SMS FindById(Guid id)
            {
										return null;
				            }
    }
}
