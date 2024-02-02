using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;

        protected BaseRepository(DataContext context)
        {
            _dataContext = context;
        }

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
