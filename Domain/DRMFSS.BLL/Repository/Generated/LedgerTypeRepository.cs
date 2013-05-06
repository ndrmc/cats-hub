using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class LedgerTypeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public LedgerTypeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.LedgerTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(LedgerType entity)
            {
                db.AddToLedgerTypes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(LedgerType entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.LedgerTypes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(LedgerType entity)
            {

                LedgerType original = db.LedgerTypes.SingleOrDefault(p => p.LedgerTypeID == entity.LedgerTypeID);
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
				                LedgerType original = db.LedgerTypes.SingleOrDefault(p => p.LedgerTypeID == id);
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

            public List<LedgerType> GetAll()
            {
                return db.LedgerTypes.ToList();
            }

            public LedgerType FindById(int id)
            {
				                		return db.LedgerTypes.ToList().SingleOrDefault(p => p.LedgerTypeID == id);
				            }
			
			public LedgerType FindById(Guid id)
            {
										return null;
				            }
    }
}
