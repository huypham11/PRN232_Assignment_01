using BusinessObjects;

namespace Services;

public interface ITagService
{
    void AddTag(Tag tag);

    void UpdateTag(int id, Tag tag);
    void DeleteTag(int id);
    Tag GetTagByID(int id);
    List<Tag> GetTags();

}