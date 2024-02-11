using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.PlayerRepositories
{
    public class RaceRepository(PlayerDataContext dataContext) : BaseRepository<RaceEntity, PlayerDataContext>(dataContext)
    {
        private readonly PlayerDataContext _dataContext = dataContext;
    }
}
