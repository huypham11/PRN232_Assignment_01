using BusinessObjects;
using Repositories;

namespace Services;

public class NewsArticleService: INewsArticleService
{
    private readonly INewsArticleRepository iNewsArticleRepository;
    public NewsArticleService()
    {
        iNewsArticleRepository = new NewsArticleRepository();
    }
    public List<NewsArticle> GetNewsArticles() => iNewsArticleRepository.GetNewsArticles();
    public NewsArticle GetNewsArticleByID(string id) => iNewsArticleRepository.GetNewsArticleByID(id);
    public void AddNewsArticle(NewsArticle newsArticle) => iNewsArticleRepository.AddNewsArticle(newsArticle);
    public void UpdateNewsArticle(string id, NewsArticle newsArticle) => iNewsArticleRepository.UpdateNewsArticle(id, newsArticle);
    public void DeleteNewsArticle(string id) => iNewsArticleRepository.DeleteNewsArticle(id);

}