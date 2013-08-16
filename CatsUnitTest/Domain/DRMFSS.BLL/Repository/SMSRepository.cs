using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SMSRepository :GenericRepository<CTSContext,SMS>, ISMSRepository
    {
        public SMSRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.SMS.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public SMS FindById(int id)
        {
            return db.SMS.FirstOrDefault(t => t.SMSID == id);
        }

        public SMS FindById(System.Guid id)
        {
            return null;
        }
    }
}
