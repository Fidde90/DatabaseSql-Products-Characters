using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class DescriptionService(DescriptionRepository dr, ProductRepository pr)
    {
        private readonly DescriptionRepository _descriptionRepository = dr;
        private readonly ProductRepository _productRepository = pr;

        public DescriptionEntity CreateDescription(DescriptionEntity entity)
        {
            try
            {
                if (_productRepository.Exists(x => x.ArticleNumber == entity.ArticleNumber))
                {
                    var created = _descriptionRepository.AddToDB(entity);
                    if (created != null)
                        return created;
                }
            }
            catch (Exception e) { Debug.WriteLine("ERROR :: " + e); }
            return null!;
        }

        public DescriptionEntity GetDescription(string value)
        {
            try
            {
                if (_descriptionRepository.Exists(x => x.ArticleNumber == value))
                {
                    var product = _descriptionRepository.GetOneFromDB(x => x.ArticleNumber == value);
                    if (product != null)
                        return product;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }

        public bool UpdateDescription(DescriptionEntity entity)
        {
            try
            {
                if (!_descriptionRepository.Exists(x => x.ArticleNumber == entity.ArticleNumber))
                {
                    var created = CreateDescription(entity);
                    if (created != null)
                        return true;
                }
                else
                {
                    var updated = _descriptionRepository.UpdateEntityInDB(entity, x => x.ArticleNumber == entity.ArticleNumber);
                    if (updated != null)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }

        public bool DeleteDescription(string articleNumber)
        {
            try
            {
                if (_descriptionRepository.Exists(x => x.ArticleNumber == articleNumber))
                {
                    var deleted = _descriptionRepository.DeleteFromDB(x => x.ArticleNumber == articleNumber);
                    if (deleted)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
