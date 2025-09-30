using BusinessObjects;

namespace Repositories;

public class TagRepository : ITagRepository
{
    public void AddTag(Tag tag) => TagDAO.AddTag(tag);
    public void UpdateTag(int id, Tag tag) => TagDAO.UpdateTag(id, tag);
    public void DeleteTag(int id) => TagDAO.DeleteTag(id);
    public Tag GetTagByID(int id) => TagDAO.GetTagByID(id);
    public List<Tag> GetTags() => TagDAO.GetTags();

}