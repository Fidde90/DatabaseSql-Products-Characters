using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class ProductEntity
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string ArticleNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Artist { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Title { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required]
        [ForeignKey(nameof(GenreEntity))]
        public int GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryEntity))]
        public int CategoryId { get; set; }

        public virtual GenreEntity Genre { get; set; } = null!;
        public virtual CategoryEntity Category { get; set; } = null!;
        public virtual ICollection<TracklistEntity> Tracklist { get; set; } = new List<TracklistEntity>();
    }
}
