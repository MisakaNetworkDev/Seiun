using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Utils.Enums;
using Seiun.Entities;

namespace Seiun.Repositories;

public class UserWordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<UserWordRecordEntity>(dbContext, minioClient), IUserWordRepository
{
	public void BulkCreate(List<UserWordRecordEntity> records)
	{
		DbContext.UserWordRecords.AddRange(records);
	}

	public void BulkUpdate(List<UserWordRecordEntity> records)
	{
		DbContext.UserWordRecords.UpdateRange(records);
	}

    public async Task<List<Guid>> GetUserWordRecordAsync(Guid userID)
	{
		var wordIds = await DbContext.UserWordRecords
			.Where(r => r.UserId == userID)
			.Where(r => r.Stage == WordStage.Reviewing)
			.OrderByDescending(r => r.WrongCount)
			.ThenBy(r => r.NextReviewTime)
			.Select(r => r.WordId)
			.ToListAsync();

		return wordIds;
	}

	public async Task<List<UserWordRecordEntity>> BulkGetByIDAsync(List<Guid> wordIDs, Guid userID)
	{
		var wordList = await DbContext.UserWordRecords
			.Where(r => r.UserId == userID)
			.Where(r => wordIDs.Contains(r.WordId))
			.ToListAsync();
		
		return wordList;
	}
}