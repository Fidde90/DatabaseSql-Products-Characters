using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.PlayerRepositories
{
    public class WeaponRepository(PlayerDataContext dataContext) : BaseRepository<WeaponEntity, PlayerDataContext>(dataContext)
    {
        private readonly PlayerDataContext _dataContext = dataContext;
    }
}
