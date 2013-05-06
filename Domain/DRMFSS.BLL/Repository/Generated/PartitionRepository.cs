using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using System.Data;
using System;


namespace DRMFSS.BLL.Repository
{
public partial class PartitionRepository
    { 
            DRMFSSEntities1 db;
			IUnitOfWork repository;
            
            public PartitionRepository(DRMFSSEntities1 _db, IUnitOfWork uow){
                db = _db;
				repository = uow;
                //db.Partitions.MergeOption = System.Data.Objects.MergeOption.NoTracking;
            }
            
            public bool Add(Partition entity)
            {
                db.AddToPartitions(entity);
                db.SaveChanges();
                return true;
            }

            public bool SaveChanges(Partition entity)
            {
                   	if (entity.EntityState == EntityState.Detached)
                	{
                    	db.Partitions.Attach(entity);
					}
                    db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    db.SaveChanges();
                    return true;
            }


            public bool Delete(Partition entity)
            {

                Partition original = db.Partitions.SingleOrDefault(p => p.PartitionID == entity.PartitionID);
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
				                Partition original = db.Partitions.SingleOrDefault(p => p.PartitionID == id);
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

            public List<Partition> GetAll()
            {
                return db.Partitions.ToList();
            }

            public Partition FindById(int id)
            {
				                		return db.Partitions.ToList().SingleOrDefault(p => p.PartitionID == id);
				            }
			
			public Partition FindById(Guid id)
            {
										return null;
				            }
    }
}
