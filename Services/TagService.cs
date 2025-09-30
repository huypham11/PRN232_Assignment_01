using BusinessObjects;
using Repositories;

namespace Services;

public class TagService: ITagService
{
    private readonly ITagRepository iTagRepository;

    public TagService()
    {
        iTagRepository = new TagRepository();
    }
    public void AddTag(Tag tag) => iTagRepository.AddTag(tag);
    public void UpdateTag(int id, Tag tag) => iTagRepository.UpdateTag(id, tag);
    public void DeleteTag(int id) => iTagRepository.DeleteTag(id);
    public Tag GetTagByID(int id) => iTagRepository.GetTagByID(id);
    public List<Tag> GetTags() => iTagRepository.GetTags();

}