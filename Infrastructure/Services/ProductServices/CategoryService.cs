using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices
{
    public class CategoryService(CategoryRepository cs)
    {
        private readonly CategoryRepository _categoryRepository = cs;

        public List<CategoryEntity> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllFromDB().ToList();
            return categories;
        }

        public bool UpdateCategory(CategoryEntity entity)
        {
            try
            {
                var updated = _categoryRepository.UpdateEntityInDB(entity, x => x.CategoryName == entity.CategoryName);
                if (updated != null)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
