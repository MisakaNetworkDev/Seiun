using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class UserTagRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    : BaseRepository<UserTagEntity>(dbContext, minioClient), IUserTagRepository
{
    public async Task<List<TagEntity>> GetUnselectedTagsAsync(Guid userId)
    {
        var selectedTagIds = await DbContext.UserTagEntity
            .Where(ut => ut.UserId == userId)
            .Select(ut => ut.TagId)
            .ToListAsync();

        var unselectedTags = await DbContext.TagEntity
            .Where(tag => !selectedTagIds.Contains(tag.Id))
            .ToListAsync();

        return unselectedTags;
    }
    public async Task<List<TagEntity>> GetSelectedTagsAsync(Guid userId)
    {
        var selectedTags = await DbContext.UserTagEntity
            .Where(ut => ut.UserId == userId)
            .Select(ut => ut.Tag)
            .ToListAsync();

        return selectedTags;
    }
    
    public async Task CancelTagAsync(Guid userId, Guid tagId)
    {
        var userTag = await DbContext.UserTagEntity
            .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagId == tagId);

        if (userTag != null)
        {
            DbContext.UserTagEntity.Remove(userTag);
        }
    }
}
