using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class LedgerRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public LedgerRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Ledgers.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Ledger entity)
            {
                db.AddToLedgers(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Ledger entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Ledgers.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Ledger entity)
            {

                Ledger original = db.Ledgers.SingleOrDefault(p => p.LedgerID == entity.LedgerID);
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
				                Ledger original = db.Ledgers.SingleOrDefault(p => p.LedgerID == id);
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

            public List<Ledger> GetAll()
            {
                return db.Ledgers.ToList();
            }

            public Ledger FindById(int id)
            {
				                		return db.Ledgers.ToList().SingleOrDefault(p => p.LedgerID == id);
				            }
			
			public Ledger FindById(Guid id)
            {
										return null;
				            }
    }
}
