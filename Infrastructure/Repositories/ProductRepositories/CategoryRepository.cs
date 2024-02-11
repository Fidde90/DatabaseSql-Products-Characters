using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryRepository(DataContext dc) : BaseRepository<CategoryEntity,DataContext>(dc)
    {
        private readonly DataContext _dataContext = dc;
    }
}
