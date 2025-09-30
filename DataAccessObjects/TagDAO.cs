namespace BusinessObjects;

public class TagDAO
{
    public static List<Tag> GetTags()
    {
        List<Tag> tags;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                tags = context.Tags.ToList();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return tags;
    }
    public static Tag GetTagByID(int id)
    {
        Tag tag = null;
        try
        {
            using (var context = new FunewsManagementContext())
            {
                tag = context.Tags.FirstOrDefault(t => t.TagId == id);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return tag;
    }
    public static void AddTag(Tag tag)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public static void UpdateTag(int id, Tag tag)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var t = context.Tags.FirstOrDefault(x => x.TagId == id);
                if (t == null)
                {
                    throw new Exception("Tag not found.");
                }
                t.TagName = tag.TagName;
                t.Note = tag.Note;
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public static void DeleteTag(int id)
    {
        try
        {
            using (var context = new FunewsManagementContext())
            {
                var tag = context.Tags.FirstOrDefault(x => x.TagId == id);
                if (tag == null)
                {
                    throw new Exception("Tag not found.");
                }
                context.Tags.Remove(tag);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}