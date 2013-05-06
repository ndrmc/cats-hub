using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ProgramRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ProgramRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Programs.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Program entity)
            {
                db.AddToPrograms(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Program entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Programs.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Program entity)
            {

                Program original = db.Programs.SingleOrDefault(p => p.ProgramID == entity.ProgramID);
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
				                Program original = db.Programs.SingleOrDefault(p => p.ProgramID == id);
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

            public List<Program> GetAll()
            {
                return db.Programs.ToList();
            }

            public Program FindById(int id)
            {
				                		return db.Programs.ToList().SingleOrDefault(p => p.ProgramID == id);
				            }
			
			public Program FindById(Guid id)
            {
										return null;
				            }
    }
}
