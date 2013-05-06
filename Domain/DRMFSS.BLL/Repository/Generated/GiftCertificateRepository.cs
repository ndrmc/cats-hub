using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class GiftCertificateRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public GiftCertificateRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.GiftCertificates.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(GiftCertificate entity)
            {
                db.AddToGiftCertificates(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(GiftCertificate entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.GiftCertificates.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(GiftCertificate entity)
            {

                GiftCertificate original = db.GiftCertificates.SingleOrDefault(p => p.GiftCertificateID == entity.GiftCertificateID);
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
				                GiftCertificate original = db.GiftCertificates.SingleOrDefault(p => p.GiftCertificateID == id);
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

            public List<GiftCertificate> GetAll()
            {
                return db.GiftCertificates.ToList();
            }

            public GiftCertificate FindById(int id)
            {
				                		return db.GiftCertificates.ToList().SingleOrDefault(p => p.GiftCertificateID == id);
				            }
			
			public GiftCertificate FindById(Guid id)
            {
										return null;
				            }
    }
}
