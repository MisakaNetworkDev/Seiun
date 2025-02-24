using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class UserArticleStatusRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<UserArticleStatusEntity>(dbContext, minioClient), IUserArticleStatusRepository
{
	public async Task<List<Guid>?> GetArticleListByLikedRecordAsync(Guid userId)
	{
		var articleList = await DbContext.UserArticleStatus
			.Where(a => a.UserId == userId)
			.OrderByDescending(a => a.LikedTime)
			.ToListAsync();

		List<Guid> articleIds = [];
		for(int i = 0; i < articleList.Count; i++)
		{
			articleIds.Add(articleList[i].LikedArticleId);
		}
		return articleIds;
	}

	public async Task<UserArticleStatusEntity?> GetArticleByLikedRecordAsync(Guid userId, Guid articleId)
	{
		return await DbContext.UserArticleStatus
			.Where(a => a.UserId == userId && a.LikedArticleId == articleId)
			.FirstOrDefaultAsync();
	}

	public async Task<int> GetUserCountByLikedRecordAsync(Guid articleId)
	{
		var userCount = await DbContext.UserArticleStatus
			.Where(a => a.LikedArticleId == articleId)
			.CountAsync();
			
		return userCount;
	}
}