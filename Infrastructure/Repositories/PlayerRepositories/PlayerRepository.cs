using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.PlayerRepositories
{
    public class PlayerRepository(PlayerDataContext dataContext) : BaseRepository<PlayerEntity, PlayerDataContext>(dataContext)
    {
        private readonly PlayerDataContext _dataContext = dataContext;

        /// <summary>
        /// Deletes a player and weapons from the database.
        /// </summary>
        /// <param name="predicate"> Lambda Expression </param>
        /// <returns>Returns a boolean value</returns>
        public override bool DeleteFromDB(Expression<Func<PlayerEntity, bool>> predicate)
        {
            try
            {
                var entity = _dataContext.Players.Include(player => player.PlayerWeapons).FirstOrDefault(predicate);

                if (entity != null)
                {
                    _dataContext.PlayerWeapons.RemoveRange(entity.PlayerWeapons);
                    _dataContext.Players.Remove(entity);
                    _dataContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return false;
        }

        /// <summary>
        ///     Gets all players from the database.
        /// </summary>
        /// <returns>Returns a list of all Players and includes Kind and Race</returns>
        public override IEnumerable<PlayerEntity> GetAllFromDB()
        {
            try
            {
                var list = _dataContext.Players.Include(player => player.KindNavigation).Include(player => player.RaceNavigation).Include(x => x.PlayerWeapons).ToList();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
    }
}
