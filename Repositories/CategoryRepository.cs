using BusinessObjects;

namespace Repositories;

public class CategoryRepository : ICategoryRepository
{
    public void AddCategory(Category c) => CategoryDAO.AddCategory(c);
    public void UpdateCategory(int id, Category c) => CategoryDAO.UpdateCategory(id, c);
    public void DeleteCategory(int id) => CategoryDAO.DeleteCategory(id);
    public Category GetCategoryByID(int id) => CategoryDAO.GetCategoryByID(id);
    public List<Category> GetCategories() => CategoryDAO.GetCategories();


}