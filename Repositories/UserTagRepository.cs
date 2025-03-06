using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class UserTagRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    : BaseRepository<UserTagEntity>(dbContext, minioClient), IUserTagRepository
{
    public async Task<List<TagEntity>> GetUnselectedTagsAsync(Guid userId)
    {
        var selectedTagIds = await DbContext.UserTag
            .Where(ut => ut.UserId == userId)
            .Select(ut => ut.TagId)
            .ToListAsync();

        var unselectedTags = await DbContext.Tag
            .Where(tag => !selectedTagIds.Contains(tag.Id))
            .ToListAsync();

        return unselectedTags;
    }
    public async Task<List<UserTagEntity>> GetSelectedTagsAsync(Guid userId)
    {
        var selectedTags = await DbContext.UserTag
            .Where(ut => ut.UserId == userId)
            .ToListAsync();

        return selectedTags;
    }
    
    public async Task CancelTagAsync(Guid userId, Guid tagId)
    {
        var userTag = await DbContext.UserTag
            .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagId == tagId);

        if (userTag != null)
        {
            DbContext.UserTag.Remove(userTag);
        }
    }

    public async Task<UserTagEntity?> GetStudyingTagByUserIdAsync(Guid userId)
    {
        return await DbContext.UserTag
            .Where(ut => ut.UserId == userId)
            .OrderByDescending(ut => ut.SettingAt)  
            .FirstOrDefaultAsync(); 
    }
}
