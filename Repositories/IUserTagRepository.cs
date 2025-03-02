using Seiun.Entities;

namespace Seiun.Repositories;

public interface IUserTagRepository : IBaseRepository<UserTagEntity>
{
    Task<List<TagEntity>> GetUnselectedTagsAsync(Guid userId);
    Task<List<TagEntity>> GetSelectedTagsAsync(Guid userId);
    Task CancelTagAsync(Guid userId, Guid tagId);
}