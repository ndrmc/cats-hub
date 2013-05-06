using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class GiftCertificateDetailRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public GiftCertificateDetailRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.GiftCertificateDetails.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(GiftCertificateDetail entity)
            {
                db.AddToGiftCertificateDetails(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(GiftCertificateDetail entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.GiftCertificateDetails.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(GiftCertificateDetail entity)
            {

                GiftCertificateDetail original = db.GiftCertificateDetails.SingleOrDefault(p => p.GiftCertificateDetailID == entity.GiftCertificateDetailID);
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
				                GiftCertificateDetail original = db.GiftCertificateDetails.SingleOrDefault(p => p.GiftCertificateDetailID == id);
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

            public List<GiftCertificateDetail> GetAll()
            {
                return db.GiftCertificateDetails.ToList();
            }

            public GiftCertificateDetail FindById(int id)
            {
				                		return db.GiftCertificateDetails.ToList().SingleOrDefault(p => p.GiftCertificateDetailID == id);
				            }
			
			public GiftCertificateDetail FindById(Guid id)
            {
										return null;
				            }
    }
}
