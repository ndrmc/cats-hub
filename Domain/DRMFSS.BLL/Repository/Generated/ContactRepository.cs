using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ContactRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ContactRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Contacts.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Contact entity)
            {
                db.AddToContacts(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Contact entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Contacts.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Contact entity)
            {

                Contact original = db.Contacts.SingleOrDefault(p => p.ContactID == entity.ContactID);
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
				                Contact original = db.Contacts.SingleOrDefault(p => p.ContactID == id);
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

            public List<Contact> GetAll()
            {
                return db.Contacts.ToList();
            }

            public Contact FindById(int id)
            {
				                		return db.Contacts.ToList().SingleOrDefault(p => p.ContactID == id);
				            }
			
			public Contact FindById(Guid id)
            {
										return null;
				            }
    }
}
