using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class DescriptionEntity
    {
        [Key]
        [ForeignKey(nameof(ProductEntity))]
        public string ArticleNumber { get; set; } = null!;

        public string? About { get; set; }

        public static implicit operator DescriptionEntity(Product product)
        {
            DescriptionEntity description = new DescriptionEntity
            {
                ArticleNumber = product.ArticleNumber,
                About = product.About
            };

            return description;
        }
    }
}
