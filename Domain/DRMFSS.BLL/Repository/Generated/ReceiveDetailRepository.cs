using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ReceiveDetailRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ReceiveDetailRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ReceiveDetails.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ReceiveDetail entity)
            {
                db.AddToReceiveDetails(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ReceiveDetail entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ReceiveDetails.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ReceiveDetail entity)
            {

                ReceiveDetail original = db.ReceiveDetails.SingleOrDefault(p => p.ReceiveDetailID == entity.ReceiveDetailID);
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
										return false;
					            }

			public bool DeleteByID(Guid id)
            {
				                	ReceiveDetail original = db.ReceiveDetails.SingleOrDefault(p => p.ReceiveDetailID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<ReceiveDetail> GetAll()
            {
                return db.ReceiveDetails.ToList();
            }

            public ReceiveDetail FindById(int id)
            {
										return null;
				            }
			
			public ReceiveDetail FindById(Guid id)
            {
				                		return db.ReceiveDetails.ToList().SingleOrDefault(p => p.ReceiveDetailID == id);
				            }
    }
}
