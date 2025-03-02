using Seiun.Entities;

namespace Seiun.Repositories;
public interface  IUserWordRepository : IBaseRepository<UserWordRecordEntity>
{
    void BulkCreate(List<UserWordRecordEntity> records);
	void BulkUpdate(List<UserWordRecordEntity> records);
    Task<List<Guid>> GetUserWordRecordAsync(Guid userID);
	Task<List<UserWordRecordEntity>> BulkGetByIDAsync(List<Guid> wordIDs, Guid userID);
}