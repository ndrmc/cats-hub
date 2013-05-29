using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.BLL.ViewModels.Report;
using DRMFSS.BLL.ViewModels.Common;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProgramRepository :GenericRepository<CTSContext,Program> ,IProgramRepository
    {
        public ProgramRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        } 
        public List<ProgramViewModel> GetAllProgramsForReport()
        {
            var programs = (from c in db.Programs select new ProgramViewModel() { ProgramId = c.ProgramID, ProgramName = c.Name }).ToList();
            programs.Insert(0 , new ProgramViewModel { ProgramName = "All Programs" });
            return programs;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.Programs.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public Program FindById(int id)
        {
            return db.Programs.FirstOrDefault(t => t.ProgramID == id);
        }

        public Program FindById(System.Guid id)
        {
            return null;

        }
    }
}
