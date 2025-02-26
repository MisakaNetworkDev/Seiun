using Seiun.Entities;

namespace Seiun.Repositories;
public interface  IUserWordRepository : IBaseRepository<UserWordRecordEntity>
{
    void BulkCreate(List<UserWordRecordEntity> words);
	void BulkUpdate(List<UserWordRecordEntity> words);
    Task<List<UserWordRecordEntity>> BulkGetAsync();
	Task<List<UserWordRecordEntity>> BulkGetByIDAsync(List<Guid> wordIDs, Guid userID);
}