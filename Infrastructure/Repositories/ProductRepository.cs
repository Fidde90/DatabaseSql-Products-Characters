using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dc) : base(dc)
        {
            _dataContext = dc;
        }

        public override IEnumerable<ProductEntity> GetAllFromDB()
        {
            var listToReturn = _dataContext.Products.Include(x => x.Genre).Include(y => y.Category).ToList();
            return listToReturn;
        }
    }
}
