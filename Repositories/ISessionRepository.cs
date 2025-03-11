using Seiun.Entities;

namespace Seiun.Repositories;

public interface ISessionRepository : IBaseRepository<WordSessionEntity>
{
	Task<WordSessionEntity?> GetSessionByUserIdAsync(Guid userId);
}