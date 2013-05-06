using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class CommodityTypeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public CommodityTypeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.CommodityTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(CommodityType entity)
            {
                db.AddToCommodityTypes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(CommodityType entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.CommodityTypes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(CommodityType entity)
            {

                CommodityType original = db.CommodityTypes.SingleOrDefault(p => p.CommodityTypeID == entity.CommodityTypeID);
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
				                CommodityType original = db.CommodityTypes.SingleOrDefault(p => p.CommodityTypeID == id);
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

            public List<CommodityType> GetAll()
            {
                return db.CommodityTypes.ToList();
            }

            public CommodityType FindById(int id)
            {
				                		return db.CommodityTypes.ToList().SingleOrDefault(p => p.CommodityTypeID == id);
				            }
			
			public CommodityType FindById(Guid id)
            {
										return null;
				            }
    }
}
