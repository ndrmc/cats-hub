using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;

namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TransactionGroupRepository:GenericRepository<CTSContext,TransactionGroup>, ITransactionGroupRepository
    {
        public TransactionGroupRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public Guid GetLastTrasactionGroupId()
        {
            Guid trasactionGroupId = db.TransactionGroups.OrderByDescending(c => c.TransactionGroupID).Select(x => x.TransactionGroupID).First();
            return trasactionGroupId;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.TransactionGroups.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public TransactionGroup FindById(int id)
        {
            return null;

        }

        public TransactionGroup FindById(System.Guid id)
        {
 return db.TransactionGroups.FirstOrDefault(t => t.TransactionGroupID == id);
           
        }
    }
}
