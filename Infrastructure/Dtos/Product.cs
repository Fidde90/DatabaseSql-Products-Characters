using Infrastructure.Entities;

namespace Infrastructure.Dtos
{
    public class Product
    {
        public string ArticleNumber { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Genre { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? About { get; set; }
        public string? ReleseDate { get; set; }

        public string? RecordedDate { get; set; }

        public string? Country { get; set; }

        public string? Label { get; set; }

        public static implicit operator Product(ProductEntity entity)
        {
            Product product = new Product
            {
                ArticleNumber = entity.ArticleNumber,
                Artist = entity.Artist,
                Title = entity.Title,
                Price = entity.Price,
                Quantity = entity.InStock,
                Genre = entity.Genre.GenreName,
                Category = entity.Category.CategoryName,
            };

            return product;
        }
    }
}


