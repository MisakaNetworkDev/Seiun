using Seiun.Entities;

namespace Seiun.Repositories;

public interface IUserArticleStatusRepository : IBaseRepository<UserArticleStatusEntity>
{
	Task<List<Guid>?> GetArticleListByLikedRecordAsync(Guid userId);
	Task<UserArticleStatusEntity?> GetArticleByLikedRecordAsync(Guid userId, Guid articleId);
	Task<int> GetUserCountByLikedRecordAsync(Guid articleId);
}