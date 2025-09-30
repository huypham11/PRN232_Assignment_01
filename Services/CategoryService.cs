using BusinessObjects;
using Repositories;

namespace Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository iCategoryRepository;
    public CategoryService()
    {
        iCategoryRepository = new CategoryRepository();
    }
    public void AddCategory(Category c) => iCategoryRepository.AddCategory(c);
    public void UpdateCategory(int id, Category c) => iCategoryRepository.UpdateCategory(id, c);
    public void DeleteCategory(int id) => iCategoryRepository.DeleteCategory(id);
    public Category GetCategoryByID(int id) => iCategoryRepository.GetCategoryByID(id);
    public List<Category> GetCategories() => iCategoryRepository.GetCategories();

}