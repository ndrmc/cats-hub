
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IPeriodService
    {

        bool AddPeriod(Period entity);
        bool DeletePeriod(Period entity);
        bool DeleteById(int id);
        bool EditPeriod(Period entity);
        Period FindById(int id);
        List<Period> GetAllPeriod();
        List<Period> FindBy(Expression<Func<Period, bool>> predicate);
        public List<int?> GetYears();
        public List<int?> GetMonths(int year);
        BLL.Period GetPeriod(int year, int month);

    }
}


