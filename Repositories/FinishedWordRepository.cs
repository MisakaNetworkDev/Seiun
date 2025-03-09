using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class FinishedWordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<FinishedWordRecordEntity>(dbContext, minioClient), IFinishedWordRepository
{
    public async Task<IGrouping<Guid,FinishedWordRecordEntity>?> GetLatestFinishedWordIdAsync(Guid userId)
	{	
		var LatestFinishedWords = await DbContext.FinishedWords
			.Where(x => x.UserId == userId)
			.OrderByDescending(x => x.FinishedAt)
			.ToListAsync();
		
		return LatestFinishedWords
			.GroupBy(x => x.SessionId)
			.FirstOrDefault();
	}
}