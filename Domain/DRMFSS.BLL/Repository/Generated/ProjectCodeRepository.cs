using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ProjectCodeRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ProjectCodeRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ProjectCodes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ProjectCode entity)
            {
                db.AddToProjectCodes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ProjectCode entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ProjectCodes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ProjectCode entity)
            {

                ProjectCode original = db.ProjectCodes.SingleOrDefault(p => p.ProjectCodeID == entity.ProjectCodeID);
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
				                ProjectCode original = db.ProjectCodes.SingleOrDefault(p => p.ProjectCodeID == id);
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

            public List<ProjectCode> GetAll()
            {
                return db.ProjectCodes.ToList();
            }

            public ProjectCode FindById(int id)
            {
				                		return db.ProjectCodes.ToList().SingleOrDefault(p => p.ProjectCodeID == id);
				            }
			
			public ProjectCode FindById(Guid id)
            {
										return null;
				            }
    }
}
