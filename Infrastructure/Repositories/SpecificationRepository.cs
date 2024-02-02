using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SpecificationRepository : BaseRepository<SpecificationEntity>
    {
        private readonly DataContext _dataContext;

        public SpecificationRepository(DataContext dc) : base(dc)
        {
            _dataContext = dc;
        }
    }
}
