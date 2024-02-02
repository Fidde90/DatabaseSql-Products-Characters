using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class TracklistRepository : BaseRepository<TracklistEntity>
    {
        private readonly DataContext _dataContext;

        public TracklistRepository(DataContext dc) : base(dc)
        {
            _dataContext = dc;
        }

        public virtual IEnumerable<TracklistEntity> GetAll_TracksFromDB(Expression<Func<TracklistEntity, bool>> predicate)
        {
            try
            {
                var list = _dataContext.Set<TracklistEntity>().ToList();
                if (list != null)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
    }
}
