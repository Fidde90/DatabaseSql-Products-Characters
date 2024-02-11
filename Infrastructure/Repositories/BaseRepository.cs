using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity,TContext> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _dataContext;

        protected BaseRepository(TContext context)
        {
            _dataContext = context;
        }

        /// <summary>
        /// Adds one Entity to the database
        /// </summary>
        /// <param name="entity">The Entity to add</param>
        /// <param name="asNoTracking"></param>
        /// <returns>returns an Entity</returns>
        public virtual TEntity AddToDB(TEntity entity, bool asNoTracking = false)
        {
            try
            {
                if (asNoTracking)
                    _dataContext.Entry(entity).State = EntityState.Detached;

                _dataContext.Set<TEntity>().Add(entity);
                _dataContext.SaveChanges();
                return entity;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }

        /// <summary>
        /// Gets all Entities of a type in the database.
        /// </summary>
        /// <returns>IEnumerable list</returns>
        public virtual IEnumerable<TEntity> GetAllFromDB()
        {
            try
            {
                var list = _dataContext.Set<TEntity>().ToList();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
      
        /// <summary>
        /// Gets one Entity of a given type from the database.
        /// </summary>
        /// <param name="predicate">Lambda expression</param>
        /// <returns>returns an Entity</returns>
        public virtual TEntity GetOneFromDB(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = _dataContext.Set<TEntity>().FirstOrDefault(predicate);
                if (entity != null)
                    return entity;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }

        /// <summary>
        /// Updates an Entity of a given type in the database.
        /// </summary>
        /// <param name="entity">the entity with the new values</param>
        /// <param name="predicate">Lambda expression</param>
        /// <returns>returns the updated Entity</returns>
        public virtual TEntity UpdateEntityInDB(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var update = _dataContext.Set<TEntity>().FirstOrDefault(predicate);
                if (update != null)
                {                   
                    _dataContext.Entry(update).CurrentValues.SetValues(entity);
                    _dataContext.SaveChanges();
                    return update;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }

        /// <summary>
        /// Deletes on Entity of a given type in the database.
        /// </summary>
        /// <param name="predicate">Lambda expression</param>
        /// <returns>return a boolean value, true if the entity is deleted, else false</returns>
        public virtual bool DeleteFromDB(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = _dataContext.Set<TEntity>().FirstOrDefault(predicate);

                if (entity != null)
                {
                    _dataContext.Set<TEntity>().Remove(entity);
                    _dataContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return false;
        }

        /// <summary>
        /// Checks if an entity of a given type exists in the database.
        /// </summary>
        /// <param name="predicate">Lambda expression</param>
        /// <returns>returns a boolean value, true if the entity was found, else false</returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var Exists = _dataContext.Set<TEntity>().AsNoTracking().Any(predicate);
                if (Exists)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return false;
        }
    }
}
