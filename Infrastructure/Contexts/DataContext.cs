using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<GenreEntity> Genres { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }

        public virtual DbSet<ProductEntity> Products { get; set; }

        public virtual DbSet<DescriptionEntity> Descriptions { get; set; }

        public virtual DbSet<SpecificationEntity> Specifications { get; set; }

        public virtual DbSet<TracklistEntity> Tracklists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TracklistEntity>()
                .HasKey(x => new { x.ArticleNumber, x.Title });    
        }
    }
}
