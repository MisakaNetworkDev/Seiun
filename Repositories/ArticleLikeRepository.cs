using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class UserArticleStatusRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<ArticleLikeEntity>(dbContext, minioClient), IArticleLikeRepository
{
	public async Task<List<Guid>?> GetArticleListByLikedRecordAsync(Guid userId)
	{
		var articleList = await DbContext.ArticleLikes
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

	public async Task<ArticleLikeEntity?> GetArticleByLikedRecordAsync(Guid userId, Guid articleId)
	{
		return await DbContext.ArticleLikes
			.Where(a => a.UserId == userId && a.LikedArticleId == articleId)
			.FirstOrDefaultAsync();
	}

	public async Task<int> GetUserCountByLikedRecordAsync(Guid articleId)
	{
		var userCount = await DbContext.ArticleLikes
			.Where(a => a.LikedArticleId == articleId)
			.CountAsync();
			
		return userCount;
	}
}