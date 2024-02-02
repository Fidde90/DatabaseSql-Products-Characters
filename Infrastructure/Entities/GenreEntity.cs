using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    [Index(nameof(GenreName), IsUnique = true)]
    public class GenreEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string GenreName { get; set; } = null!;

        public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();

        public static implicit operator GenreEntity(Product product)
        {
            GenreEntity specification = new GenreEntity
            {
                GenreName = product.Genre
            };

            return specification;
        }
    }
}
