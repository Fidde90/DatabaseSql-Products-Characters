using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class DescriptionRepository : BaseRepository<DescriptionEntity>
    {
        private readonly DataContext _dataContext;

        public DescriptionRepository(DataContext dc) : base(dc)
        {
            _dataContext = dc;
        }
    }
}
