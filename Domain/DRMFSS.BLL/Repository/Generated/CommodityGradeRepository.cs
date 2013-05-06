using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class CommodityGradeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public CommodityGradeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.CommodityGrades.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(CommodityGrade entity)
            {
                db.AddToCommodityGrades(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(CommodityGrade entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.CommodityGrades.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(CommodityGrade entity)
            {

                CommodityGrade original = db.CommodityGrades.SingleOrDefault(p => p.CommodityGradeID == entity.CommodityGradeID);
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
				                CommodityGrade original = db.CommodityGrades.SingleOrDefault(p => p.CommodityGradeID == id);
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

            public List<CommodityGrade> GetAll()
            {
                return db.CommodityGrades.ToList();
            }

            public CommodityGrade FindById(int id)
            {
				                		return db.CommodityGrades.ToList().SingleOrDefault(p => p.CommodityGradeID == id);
				            }
			
			public CommodityGrade FindById(Guid id)
            {
										return null;
				            }
    }
}
