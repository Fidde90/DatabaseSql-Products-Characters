using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();

        public static implicit operator CategoryEntity(Product product)
        {
            CategoryEntity specification = new CategoryEntity
            {
                CategoryName = product.Category       
            };

            return specification;
        }
    }
}
