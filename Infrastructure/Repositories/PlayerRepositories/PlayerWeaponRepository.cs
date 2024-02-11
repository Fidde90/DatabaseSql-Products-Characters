using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.PlayerRepositories
{
    public class PlayerWeaponRepository(PlayerDataContext dataContext) : BaseRepository<PlayerWeaponEntity, PlayerDataContext>(dataContext)
    {
        private readonly PlayerDataContext _dataContext = dataContext;

        public IEnumerable<PlayerWeaponEntity> GetAllPlayerWeaponsFromDB(Expression<Func<PlayerWeaponEntity,bool>> predicate)
        {
            try
            {
                var list = _dataContext.PlayerWeapons.ToList();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
    }
}
