using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class CommodityRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public CommodityRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Commodities.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Commodity entity)
            {
                db.AddToCommodities(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Commodity entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Commodities.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Commodity entity)
            {

                Commodity original = db.Commodities.SingleOrDefault(p => p.CommodityID == entity.CommodityID);
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
				                Commodity original = db.Commodities.SingleOrDefault(p => p.CommodityID == id);
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

            public List<Commodity> GetAll()
            {
                return db.Commodities.ToList();
            }

            public Commodity FindById(int id)
            {
				                		return db.Commodities.ToList().SingleOrDefault(p => p.CommodityID == id);
				            }
			
			public Commodity FindById(Guid id)
            {
										return null;
				            }
    }
}
