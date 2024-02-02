using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ProductService(ProductRepository pr, GenreRepository gr, CategoryRepository cr, TracklistService ts, SpecificationService ss, DescriptionService ds, DescriptionRepository dr, SpecificationRepository sr)
    {
        private readonly ProductRepository _productRepository = pr;
        private readonly GenreRepository _genreRepository = gr;
        private readonly CategoryRepository _categoryRepository = cr;
        private readonly TracklistService _tracklistService = ts;
        private readonly SpecificationService _specificationService = ss;
        private readonly DescriptionService _descriptionService = ds;
        private readonly DescriptionRepository _descriptiontRepository = dr;
        private readonly SpecificationRepository _specificationRepository = sr;

        public Product CurrentProduct { get; set; } = null!;

        public bool CreateProduct(Product product, List<TracklistEntity>? tracks = null)
        {
            try
            {
                var converted = ConvertTo_ProductEntity(product);

                if (converted != null && !_productRepository.Exists(x => x.ArticleNumber == converted.ArticleNumber))
                {
                    var result = _productRepository.AddToDB(converted, true);

                    if (tracks!.Count > 0)
                        _tracklistService.CreateTracks(tracks);

                    if (!string.IsNullOrWhiteSpace(product.About))
                        _descriptionService.CreateDescription(product);

                    if (product.Label != null || product.ReleseDate != null || product.RecordedDate != null || product.Country != null)
                        _specificationService.CreateSpecification(product);

                    if (result != null)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("ERROR :: " + e); }
            return false;
        }

        public List<ProductEntity> GetAllProducts()
        {
            try
            {
                var list = _productRepository.GetAllFromDB().ToList();
                return list;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }

        public Product GetProduct(string value)
        {
            try
            {
                var product = _productRepository.GetOneFromDB(x => x.ArticleNumber == value);
                if (product != null)
                    return product;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }

        public bool UpdateProduct(ProductEntity entity)
        {          
            try
            {
                var updated = _productRepository.UpdateEntityInDB(entity, x => x.ArticleNumber == entity.ArticleNumber);
                if (updated != null)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }

        public bool DeleteProduct(string articleNumber)
        {
            try
            {
                List<bool> errorList = new List<bool>();

                if (_productRepository.Exists(x => x.ArticleNumber == articleNumber))
                {
                    errorList.Add(_productRepository.DeleteFromDB(x => x.ArticleNumber == articleNumber));

                    if (_descriptiontRepository.Exists(d => d.ArticleNumber == articleNumber))
                        errorList.Add(_descriptionService.DeleteDescription(articleNumber));

                    if (_specificationRepository.Exists(s => s.ArticleNumber == articleNumber))
                        errorList.Add(_specificationService.DeleteSpecification(articleNumber));
                }


                if (!errorList.Contains(false))
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }

        public ProductEntity ConvertTo_ProductEntity(Product product)
        {
            try
            {
                var genre = _genreRepository.GetOneFromDB(x => x.GenreName == product.Genre);
                var category = _categoryRepository.GetOneFromDB(x => x.CategoryName == product.Category);

                if(genre != null && category != null)
                {
                    ProductEntity productEntity = new ProductEntity
                    {
                        ArticleNumber = product.ArticleNumber,
                        Artist = product.Artist,
                        Title = product.Title,
                        Price = product.Price,
                        InStock = product.Quantity,
                        GenreId = genre.Id,
                        CategoryId = category.Id
                    };
                    return productEntity;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }
    }
}



