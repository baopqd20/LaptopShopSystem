using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface ICategoryRepository
    {
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        Category GetCategoryById(int id);
        bool CategoryExists(int id);
        ICollection<Category> GetCategories();
        bool Save();
    }
}
