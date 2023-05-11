using GameStoreApp.Models;
using System.Linq.Expressions;

namespace GameStoreApp.Data.Base
{
    /// <summary>
    /// Represents a repository for entities that implement the IEntityBase interface.
    /// </summary>
    /// <typeparam name="T">The type of entity in the repository.</typeparam>
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        /// <summary>
        /// Gets all entities in the entity table of T.
        /// </summary>
        /// <returns>An IEnumerable of T.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets all entities in the  entity table of T and includes related entities specified by the includeProperties parameter.
        /// </summary>
        /// <param name="includeProperties">The related entities to include.</param>
        /// <returns>An IEnumerable of T.</returns>
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Gets an entity with the specified id.
        /// </summary>
        /// <param name="id">The id of the entity to get.</param>
        /// <returns>A T object.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        Task UpdateAsync(int id, T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        Task DeleteAsync(int id);
    }
}
