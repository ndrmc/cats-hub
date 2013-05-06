using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class LetterTemplateRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public LetterTemplateRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.LetterTemplates.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(LetterTemplate entity)
            {
                db.AddToLetterTemplates(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(LetterTemplate entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.LetterTemplates.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(LetterTemplate entity)
            {

                LetterTemplate original = db.LetterTemplates.SingleOrDefault(p => p.LetterTemplateID == entity.LetterTemplateID);
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
				                LetterTemplate original = db.LetterTemplates.SingleOrDefault(p => p.LetterTemplateID == id);
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

            public List<LetterTemplate> GetAll()
            {
                return db.LetterTemplates.ToList();
            }

            public LetterTemplate FindById(int id)
            {
				                		return db.LetterTemplates.ToList().SingleOrDefault(p => p.LetterTemplateID == id);
				            }
			
			public LetterTemplate FindById(Guid id)
            {
										return null;
				            }
    }
}
