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
   public partial class LedgerRepository: GenericRepository<CTSContext,Ledger>,ILedgerRepository
    {
       public LedgerRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

       public bool DeleteByID(int id)
       {
           var original = FindById(id);
           if (original == null) return false;
           db.Ledgers.Remove(original);

           return true;
       }

       public bool DeleteByID(System.Guid id)
       {
           return false;
       }

       public Ledger FindById(int id)
       { return db.Ledgers.FirstOrDefault(t => t.LedgerID == id);
           
       }

       public Ledger FindById(System.Guid id)
       {
          
return null;
       }
    }
}
