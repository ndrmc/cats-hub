using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class DonorRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public DonorRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Donors.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Donor entity)
            {
                db.AddToDonors(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Donor entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Donors.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Donor entity)
            {

                Donor original = db.Donors.SingleOrDefault(p => p.DonorID == entity.DonorID);
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
				                Donor original = db.Donors.SingleOrDefault(p => p.DonorID == id);
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

            public List<Donor> GetAll()
            {
                return db.Donors.ToList();
            }

            public Donor FindById(int id)
            {
				                		return db.Donors.ToList().SingleOrDefault(p => p.DonorID == id);
				            }
			
			public Donor FindById(Guid id)
            {
										return null;
				            }
    }
}
