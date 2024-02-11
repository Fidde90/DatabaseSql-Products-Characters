using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class DescriptionRepository(DataContext dc) : BaseRepository<DescriptionEntity, DataContext>(dc)
    {
        private readonly DataContext _dataContext = dc;
    }
}
