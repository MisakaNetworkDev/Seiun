using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Utils;
using Minio.DataModel.Args;
using Seiun.Entities;
using System.Net.Mime;

namespace Seiun.Repositories;

public class UserWordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<ArticleEntity>(dbContext, minioClient), IUserWordRepository
{
	public void BulkCreate(List<UserWordRecordEntity> words)
	{

	}

	public void BulkUpdate(List<UserWordRecordEntity> words)
	{

	}

    public async Task<List<UserWordRecordEntity>> BulkGetAsync()
	{

	}

	public async Task<List<UserWordRecordEntity>> BulkGetByIDAsync(List<Guid> wordIDs, Guid userID)
	{
		var wordList = await dbContext.UserWordRecords
			.Where(r => r.UserId == userID)
			.Where(r => wordIDs.Contains(r.WordId))
			.ToListAsync();
		
		return wordList;
	}
}