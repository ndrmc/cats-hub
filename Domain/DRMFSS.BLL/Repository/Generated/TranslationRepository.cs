using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class TranslationRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public TranslationRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Translations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Translation entity)
            {
                db.AddToTranslations(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Translation entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Translations.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Translation entity)
            {

                Translation original = db.Translations.SingleOrDefault(p => p.TranslationID == entity.TranslationID);
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
				                Translation original = db.Translations.SingleOrDefault(p => p.TranslationID == id);
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

            public List<Translation> GetAll()
            {
                return db.Translations.ToList();
            }

            public Translation FindById(int id)
            {
				                		return db.Translations.ToList().SingleOrDefault(p => p.TranslationID == id);
				            }
			
			public Translation FindById(Guid id)
            {
										return null;
				            }
    }
}
