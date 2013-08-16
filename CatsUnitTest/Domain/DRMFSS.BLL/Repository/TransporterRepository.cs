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
    public partial class TransporterRepository :GenericRepository<CTSContext,Transporter>, ITransporterRepository
    {
        public TransporterRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }
        public bool IsNameValid(int? TransporterID, string Name)
        {
            return !(from v in db.Transporters
                    where v.Name == Name && v.TransporterID != TransporterID
                    select v).Any();
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Transporters.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Transporter FindById(int id)
        {
            return db.Transporters.FirstOrDefault(t => t.TransporterID == id);

        }

        public Transporter FindById(System.Guid id)
        {

            return null;
        }
    }
}
