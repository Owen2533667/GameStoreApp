using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace GameStoreApp.Data.Base
{

    /// <summary>
    /// EntityBaseRepository class is used to perform CRUD operations on the database.
    /// </summary>
    /// <typeparam name="T">The type of entity that the repository will handle.</typeparam>
    /// <remarks></remarks>
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        
        private readonly GameStoreAppDbContext _context; //private readonly attribute for the application database context.

        /// <summary>
        /// Constructor for EntityBaseRepository.
        /// </summary>
        /// <param name="context">The context of the database.</param>
        public EntityBaseRepository(GameStoreAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A task that represents the asynchronous operation of adding the entity.</returns>
        public async Task AddAsync(T entity) 
        {
            // Set<T> is a generic insert that will be replaced with the entity related to the entity passed. 
            await _context.Set<T>().AddAsync(entity); // Adds an entity to the database.
            await _context.SaveChangesAsync();  // Saves the changes
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation of deleting the entity.</returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id); // Gets the entity with the specified id.
            EntityEntry entityEntry = _context.Entry<T>(entity!); // Creates an EntityEntry object for the entity.
            entityEntry.State = EntityState.Deleted; // Sets the state of the EntityEntry object to Deleted.

            await _context.SaveChangesAsync(); // Saves the changes made to the database.
        }

        /// <summary>
        /// Gets all entities from the database.
        /// </summary>
        /// <returns>Returns a task that represents the asynchronous operation of getting all entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        /// <summary>
        /// Gets all entities from the database with specified properties included.
        /// </summary>
        /// <param name="includeProperties">The properties to include in the query.</param>
        /// <returns>A task that represents the asynchronous operation of getting a list of all entities along with the include properties passed.</returns>
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            // Creates a new queryable object for type T.
            IQueryable<T> query = _context.Set<T>();

            // Includes any specified properties in the queryable object.
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));

            // Returns all entities that match the query to a list.
            return await query.ToListAsync(); 
        }

        /// <summary>
        /// Gets an entity from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>A task that represents the asynchronous operation of getting an entity by ID.</returns>
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>A task that represents the asynchronous operation of updating an entity.</returns>
        public async Task UpdateAsync(int id, T entity) 
        {
            // Gets an EntityEntry object for the updated entity.
            EntityEntry entityEntry = _context.Entry<T>(entity);

            // Sets the state of the EntityEntry object to Modified.
            entityEntry.State = EntityState.Modified;

            // Saves changes to the database.
            await _context.SaveChangesAsync(); 
        }
    }
}
