using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class SpecificationEntity
    {
        [Key]
        [ForeignKey(nameof(ProductEntity))]
        public string ArticleNumber { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string? ReleseDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? RecordedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Country { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Label { get; set; }

        public static implicit operator SpecificationEntity(Product product)
        {
            SpecificationEntity specification = new SpecificationEntity
            {
                ArticleNumber = product.ArticleNumber,
                ReleseDate = product.ReleseDate,
                RecordedDate = product.RecordedDate,
                Country = product.Country,
                Label = product.Label
            };

            return specification;
        }
    }
}
