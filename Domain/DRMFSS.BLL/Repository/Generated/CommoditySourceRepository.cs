using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class CommoditySourceRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public CommoditySourceRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.CommoditySources.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(CommoditySource entity)
            {
                db.AddToCommoditySources(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(CommoditySource entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.CommoditySources.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(CommoditySource entity)
            {

                CommoditySource original = db.CommoditySources.SingleOrDefault(p => p.CommoditySourceID == entity.CommoditySourceID);
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
				                CommoditySource original = db.CommoditySources.SingleOrDefault(p => p.CommoditySourceID == id);
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

            public List<CommoditySource> GetAll()
            {
                return db.CommoditySources.ToList();
            }

            public CommoditySource FindById(int id)
            {
				                		return db.CommoditySources.ToList().SingleOrDefault(p => p.CommoditySourceID == id);
				            }
			
			public CommoditySource FindById(Guid id)
            {
										return null;
				            }
    }
}
