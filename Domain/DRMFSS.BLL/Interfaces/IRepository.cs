using System.Collections.Generic;
using System;

namespace DRMFSS.BLL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Add(T entity);
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool SaveChanges(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// Deletes the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        bool DeleteByID(int id);

        /// <summary>
        /// Deletes the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        bool DeleteByID(Guid id);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T FindById(int id);

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T FindById(Guid id);
    }
}
