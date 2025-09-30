using BusinessObjects;

namespace Repositories;

public interface INewsArticleRepository
{
    void AddNewsArticle(NewsArticle na);
    void UpdateNewsArticle(string id, NewsArticle na);
    void DeleteNewsArticle(string id);
    NewsArticle GetNewsArticleByID(string id);
    List<NewsArticle> GetNewsArticles();

}