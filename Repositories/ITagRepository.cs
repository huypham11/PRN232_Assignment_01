using BusinessObjects;

namespace Repositories;

public interface ITagRepository
{
    void AddTag(Tag tag);
    void UpdateTag(int id, Tag tag);
    void DeleteTag(int id);
    Tag GetTagByID(int id);
    List<Tag> GetTags();

}