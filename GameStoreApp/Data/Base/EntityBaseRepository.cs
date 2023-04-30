using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace GameStoreApp.Data.Base
{
    //This will be a base repository with genereic implementation for all other service can use.
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {

        private readonly GameStoreAppDbContext _context; //Private readonly attribute of the dbcontext.
        public EntityBaseRepository(GameStoreAppDbContext context) //constructor and inject the dbcontext.
        {
            _context = context;
        }
         
        //Generic async method called AddAsync that returns a Task.
        public async Task AddAsync(T entity) 
        {
            await _context.Set<T>().AddAsync(entity); //Set<T> is a genereic insert that will be replaced with the enetity being changed. Then add that enetity passed to the correct table.
            await _context.SaveChangesAsync(); //saves the table.
        }

        //A gereneric implementation to delete data from the database. This method is asynchronous method that returns task. The method expects a parameter of type int that will be the id of the data being removed from the database.
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id); 
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        //Asynchronous method to get all from the database entity table of <T> and return that to a list. 
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        //this also returns a list of all records in a table, but accepts parameters to be included. 
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties)); //aggregates of query including each of the parameter properties.
            return await query.ToListAsync(); //returns that to a list.
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id); //gets an entity with the provided id.

        //Updates the entity with the id with the new entity data,
        public async Task UpdateAsync(int id, T entity) 
        {
            EntityEntry entityEntry = _context.Entry<T>(entity); //creates an instance of EntityEntry, which is used to to track changes.
            entityEntry.State = EntityState.Modified; //sets the entity state to the modified state.

            await _context.SaveChangesAsync(); //saves the changes.
        }
    }
}
