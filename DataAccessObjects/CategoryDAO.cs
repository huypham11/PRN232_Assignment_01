namespace BusinessObjects;

public class CategoryDAO
{
    //get all category
    public static List<Category> GetCategories()
    {
        List<Category> categories;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                categories = context.Categories.ToList();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return categories;
    }
    //get category by ID
    public static Category GetCategoryByID(int id)
    {
        Category category = null;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                category = context.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return category;
    }
    //Add category
    public static void AddCategory(Category category)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    //Update category
    public static void UpdateCategory(int id, Category category)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var c = context.Categories.FirstOrDefault(x => x.CategoryId == id);
                if (c == null)
                {
                    throw new Exception("Category not found.");
                }
                c.CategoryName = category.CategoryName;
                c.CategoryDesciption = category.CategoryDesciption;
                c.ParentCategoryId = category.ParentCategoryId;
                c.IsActive = category.IsActive;
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    //Delete category
    public static void DeleteCategory(int id)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var c = context.Categories.FirstOrDefault(x => x.CategoryId == id);
                if (c == null)
                {
                    throw new Exception("Category not found.");
                }
                context.Categories.Remove(c);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}