using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool CategoryExists(int id)
        {
            var category = _context.Categories.Where(p => p.Id == id).FirstOrDefault();
            if (category == null)
            {
                return false;
            }
            return true;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
