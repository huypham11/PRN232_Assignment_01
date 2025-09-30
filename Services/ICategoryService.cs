using BusinessObjects;

namespace Services;

public interface ICategoryService
{
    void AddCategory(Category c);
    void UpdateCategory(int id, Category c);
    void DeleteCategory(int id);
    Category GetCategoryByID(int id);
    List<Category> GetCategories();

}