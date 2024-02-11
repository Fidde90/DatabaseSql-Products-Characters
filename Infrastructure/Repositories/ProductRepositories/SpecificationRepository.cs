using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SpecificationRepository(DataContext dc) : BaseRepository<SpecificationEntity, DataContext>(dc)
    {
        private readonly DataContext _dataContext = dc;
    }
}
