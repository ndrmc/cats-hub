using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class ShippingInstructionRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public ShippingInstructionRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.ShippingInstructions.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(ShippingInstruction entity)
            {
                db.AddToShippingInstructions(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(ShippingInstruction entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.ShippingInstructions.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(ShippingInstruction entity)
            {

                ShippingInstruction original = db.ShippingInstructions.SingleOrDefault(p => p.ShippingInstructionID == entity.ShippingInstructionID);
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
				                ShippingInstruction original = db.ShippingInstructions.SingleOrDefault(p => p.ShippingInstructionID == id);
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

            public List<ShippingInstruction> GetAll()
            {
                return db.ShippingInstructions.ToList();
            }

            public ShippingInstruction FindById(int id)
            {
				                		return db.ShippingInstructions.ToList().SingleOrDefault(p => p.ShippingInstructionID == id);
				            }
			
			public ShippingInstruction FindById(Guid id)
            {
										return null;
				            }
    }
}
