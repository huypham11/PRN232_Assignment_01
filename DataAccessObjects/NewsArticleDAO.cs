namespace BusinessObjects;

public class NewsArticleDAO
{
    //get all news article
    public static List<NewsArticle> GetNewsArticles()
    {
        List<NewsArticle> newsArticles;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                newsArticles = context.NewsArticles.ToList();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return newsArticles;
    }
    //get news article by ID
    public static NewsArticle GetNewsArticleByID(string id)
    {
        NewsArticle newsArticle = null;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                newsArticle = context.NewsArticles.FirstOrDefault(n => n.NewsArticleId == id);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return newsArticle;
    }
    //Add news article
    public static void AddNewsArticle(NewsArticle newsArticle)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                //auto set created date
                newsArticle.CreatedDate = DateTime.Now;
                context.NewsArticles.Add(newsArticle);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    //Update news article
    public static void UpdateNewsArticle(string id, NewsArticle newsArticle)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var n = context.NewsArticles.FirstOrDefault(x => x.NewsArticleId == id);
                if (n == null)
                {
                    throw new Exception("News article not found.");
                }
                n.NewsTitle = newsArticle.NewsTitle;
                n.Headline = newsArticle.Headline;
                n.NewsContent = newsArticle.NewsContent;
                n.NewsSource = newsArticle.NewsSource;
                n.NewsStatus = newsArticle.NewsStatus;
                n.CategoryId = newsArticle.CategoryId;
                //auto update modified date
                n.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    //Delete news article
    public static void DeleteNewsArticle(string id)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var n = context.NewsArticles.FirstOrDefault(x => x.NewsArticleId == id);
                if (n == null)
                {
                    throw new Exception("News article not found.");
                }
                context.NewsArticles.Remove(n);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}