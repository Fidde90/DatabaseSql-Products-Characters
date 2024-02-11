using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.PlayerRepositories
{
    public class KindRepository(PlayerDataContext dataContext) : BaseRepository<KindEntity, PlayerDataContext>(dataContext)
    {
        private readonly PlayerDataContext _dataContext = dataContext;
    }
}
