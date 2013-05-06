using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class DetailRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public DetailRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Details.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Detail entity)
            {
                db.AddToDetails(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Detail entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Details.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Detail entity)
            {

                Detail original = db.Details.SingleOrDefault(p => p.DetailID == entity.DetailID);
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
				                Detail original = db.Details.SingleOrDefault(p => p.DetailID == id);
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

            public List<Detail> GetAll()
            {
                return db.Details.ToList();
            }

            public Detail FindById(int id)
            {
				                		return db.Details.ToList().SingleOrDefault(p => p.DetailID == id);
				            }
			
			public Detail FindById(Guid id)
            {
										return null;
				            }
    }
}
