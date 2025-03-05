using Seiun.Entities;

namespace Seiun.Repositories;

public interface IUserTagRepository : IBaseRepository<UserTagEntity>
{
    Task<List<TagEntity>> GetUnselectedTagsAsync(Guid userId);
    Task<List<UserTagEntity>> GetSelectedTagsAsync(Guid userId);
    Task CancelTagAsync(Guid userId, Guid tagId);
    Task<UserTagEntity?> GetStudyingTagByUserIdAsync(Guid userId);
}