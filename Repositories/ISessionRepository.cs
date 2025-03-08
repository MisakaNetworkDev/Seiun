using Seiun.Entities;

namespace Seiun.Repositories;

public interface ISessionRepository : IBaseRepository<SessionEntity>
{
	Task<SessionEntity?> GetSessionByUserIdAsync(Guid userId);
}