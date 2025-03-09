using Seiun.Entities;

namespace Seiun.Repositories;

public interface IAIArticleRepository : IBaseRepository<AIArticleEntity>
{
	Task<AIArticleEntity?> GetByUserIdAsync(Guid userId);
}