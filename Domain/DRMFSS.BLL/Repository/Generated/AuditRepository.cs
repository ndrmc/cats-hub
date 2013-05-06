using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class AuditRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public AuditRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Audits.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Audit entity)
            {
                db.AddToAudits(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Audit entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Audits.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Audit entity)
            {

                Audit original = db.Audits.SingleOrDefault(p => p.AuditID == entity.AuditID);
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
				                	Audit original = db.Audits.SingleOrDefault(p => p.AuditID == id);
	                if (original != null)
	                {
	                    db.DeleteObject(original);
	                    db.SaveChanges();
	                    return true;
	                }
	                return false;
				            }

            public List<Audit> GetAll()
            {
                return db.Audits.ToList();
            }

            public Audit FindById(int id)
            {
										return null;
				            }
			
			public Audit FindById(Guid id)
            {
				                		return db.Audits.ToList().SingleOrDefault(p => p.AuditID == id);
				            }
    }
}
