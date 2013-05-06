using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ReleaseNoteRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ReleaseNoteRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ReleaseNotes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ReleaseNote entity)
            {
                db.AddToReleaseNotes(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ReleaseNote entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ReleaseNotes.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ReleaseNote entity)
            {

                ReleaseNote original = db.ReleaseNotes.SingleOrDefault(p => p.ReleaseNoteID == entity.ReleaseNoteID);
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
				                ReleaseNote original = db.ReleaseNotes.SingleOrDefault(p => p.ReleaseNoteID == id);
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

            public List<ReleaseNote> GetAll()
            {
                return db.ReleaseNotes.ToList();
            }

            public ReleaseNote FindById(int id)
            {
				                		return db.ReleaseNotes.ToList().SingleOrDefault(p => p.ReleaseNoteID == id);
				            }
			
			public ReleaseNote FindById(Guid id)
            {
										return null;
				            }
    }
}
