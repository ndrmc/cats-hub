using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ForgetPasswordRequestRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ForgetPasswordRequestRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ForgetPasswordRequests.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ForgetPasswordRequest entity)
            {
                db.AddToForgetPasswordRequests(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ForgetPasswordRequest entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ForgetPasswordRequests.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ForgetPasswordRequest entity)
            {

                ForgetPasswordRequest original = db.ForgetPasswordRequests.SingleOrDefault(p => p.ForgetPasswordRequestID == entity.ForgetPasswordRequestID);
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
				                ForgetPasswordRequest original = db.ForgetPasswordRequests.SingleOrDefault(p => p.ForgetPasswordRequestID == id);
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

            public List<ForgetPasswordRequest> GetAll()
            {
                return db.ForgetPasswordRequests.ToList();
            }

            public ForgetPasswordRequest FindById(int id)
            {
				                		return db.ForgetPasswordRequests.ToList().SingleOrDefault(p => p.ForgetPasswordRequestID == id);
				            }
			
			public ForgetPasswordRequest FindById(Guid id)
            {
										return null;
				            }
    }
}
