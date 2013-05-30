using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace DRMFSS.BLL.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IPeriodRepository : IGenericRepository<Period>,IRepository<Period>
    {
        /// <summary>
        /// Gets the years.
        /// </summary>
        /// <returns></returns>
        List<int?> GetYears();
        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        List<int?> GetMonths(int year);
        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        BLL.Period GetPeriod(int year, int month);
    }
}
