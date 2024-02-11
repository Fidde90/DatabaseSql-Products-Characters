using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class GenreRepository(DataContext dc) : BaseRepository<GenreEntity, DataContext>(dc)
    {
        private readonly DataContext _dataContext = dc;
    }
}
