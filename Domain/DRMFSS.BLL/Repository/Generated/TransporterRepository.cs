using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class TransporterRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public TransporterRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Transporters.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Transporter entity)
            {
                db.AddToTransporters(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Transporter entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Transporters.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Transporter entity)
            {

                Transporter original = db.Transporters.SingleOrDefault(p => p.TransporterID == entity.TransporterID);
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
				                Transporter original = db.Transporters.SingleOrDefault(p => p.TransporterID == id);
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

            public List<Transporter> GetAll()
            {
                return db.Transporters.ToList();
            }

            public Transporter FindById(int id)
            {
				                		return db.Transporters.ToList().SingleOrDefault(p => p.TransporterID == id);
				            }
			
			public Transporter FindById(Guid id)
            {
										return null;
				            }
    }
}
