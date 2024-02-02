
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dc) : base(dc)
        {
            _dataContext = dc;
        }
    }
}
