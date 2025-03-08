using Seiun.Entities;

namespace Seiun.Repositories;

public interface IErrorWordRepository : IBaseRepository<ErrorWordRecordEntity>
{
	public Task<List<Guid>?> GetErrorWordIdsByUserIdAsync(Guid userId);
}