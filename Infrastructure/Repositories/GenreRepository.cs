using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<GenreEntity>
    {
        private readonly DataContext _dataContext;

        public GenreRepository(DataContext dc) : base(dc) 
        {
            _dataContext = dc;
        }
    }
}
