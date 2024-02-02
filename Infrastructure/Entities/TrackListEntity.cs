using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class TracklistEntity
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(ProductEntity))]
        public virtual string ArticleNumber { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public virtual ProductEntity Product { get; set; } = null!;
    }
}
