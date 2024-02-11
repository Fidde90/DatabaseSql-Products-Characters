using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(DataContext dc) : BaseRepository<ProductEntity, DataContext>(dc)
    {
        private readonly DataContext _dataContext = dc;

        public override IEnumerable<ProductEntity> GetAllFromDB()
        {
            var listToReturn = _dataContext.Products.Include(x => x.Genre).Include(y => y.Category).ToList();
            return listToReturn;
        }
    }
}
