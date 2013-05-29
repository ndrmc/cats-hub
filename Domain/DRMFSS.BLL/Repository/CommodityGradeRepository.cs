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
    public partial class CommodityGradeRepository :GenericRepository<CTSContext,CommodityGrade>, ICommodityGradeRepository
    {
        public CommodityGradeRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.CommodityGrades.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public CommodityGrade FindById(int id)
        {
            return db.CommodityGrades.FirstOrDefault(t => t.CommodityGradeID == id);
        }

        public CommodityGrade FindById(System.Guid id)
        {
            return null;
        }
    }
}
