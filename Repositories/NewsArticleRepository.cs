using BusinessObjects;

namespace Repositories;

public class NewsArticleRepository : INewsArticleRepository
{
    public List<NewsArticle> GetNewsArticles() => NewsArticleDAO.GetNewsArticles();
    public NewsArticle GetNewsArticleByID(string id) => NewsArticleDAO.GetNewsArticleByID(id);
    public void AddNewsArticle(NewsArticle newsArticle) => NewsArticleDAO.AddNewsArticle(newsArticle);
    public void UpdateNewsArticle(string id, NewsArticle newsArticle) => NewsArticleDAO.UpdateNewsArticle(id, newsArticle);
    public void DeleteNewsArticle(string id) => NewsArticleDAO.DeleteNewsArticle(id);

}