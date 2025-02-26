using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Utils;
using Minio.DataModel.Args;
using Seiun.Entities;
using System.Net.Mime;

namespace Seiun.Repositories;

public class ArticleRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<ArticleEntity>(dbContext, minioClient), IArticleRepository
{
	public async Task<List<Guid>?> GetArticleListAsync(int queryLength, DateTime? from)
	{
		if(queryLength > 0)
		{
			queryLength = queryLength > 10 ? 10 : queryLength;
		}
		else
		{
			queryLength = 10;
		}

		if(!from.HasValue)
		{
			from = DateTime.Now;
		}

		var articleList = await DbContext.Articles
			.Where(a => a.CreateTime <= from )
			.OrderByDescending(a => a.CreateTime)
			.Take(queryLength)
			.ToListAsync();
		
		List<Guid> articleIds = [];
		foreach(var article in articleList)
		{
			articleIds.Add(article.Id);
		}
		return articleIds;
	}

	public async Task<List<Guid>?> GetArticleListByUserIdAsync(Guid userId)
	{
		var articleList = await DbContext.Articles
			.Where(a => a.CreatorId == userId)
			.OrderByDescending(a => a.IsPinned == true)
			.ThenByDescending(a => a.PinTime)
			.ThenByDescending(a => a.CreateTime)
			.ToListAsync();

		List<Guid> articleIds = [];
		foreach(var article in articleList)
		{
			articleIds.Add(article.Id);
		}
		return articleIds;
	}

	public async Task<string> UploadArticleImgAsync(Stream articleimgData, string articleImgExtension)
	{
		string articleImgType = articleImgExtension switch
        {
            ".jpg" => MediaTypeNames.Image.Jpeg,
            ".jpeg" => MediaTypeNames.Image.Jpeg,
            ".png" => MediaTypeNames.Image.Png,
            ".webp" => MediaTypeNames.Image.Webp,
            _ => MediaTypeNames.Application.Octet 
        };
		var articleImgName = $"{Guid.NewGuid()}{articleImgExtension}";
		var putObjectArgs = new PutObjectArgs()
			.WithBucket(Constants.BucketNames.ArticleImages)
			.WithObject(articleImgName)
			.WithContentType(articleImgType)
			.WithStreamData(articleimgData)
			.WithObjectSize(articleimgData.Length);
		await MinioCl.PutObjectAsync(putObjectArgs);

		return articleImgName;
	}

	public async Task<bool> DeleteAticleImgAsync(List<string> articleImgNames)
	{
		foreach(var articleImgName in articleImgNames)
		{
			try
			{
				var removeObjectArgs = new RemoveObjectArgs()
					.WithBucket(Constants.BucketNames.ArticleImages)
					.WithObject(articleImgName);
				
				await MinioCl.RemoveObjectAsync(removeObjectArgs).ConfigureAwait(false);
			}
			catch
			{
				return false;
			}		
		}

		return true;
	}

	public async Task<MemoryStream> GetArticleImgAsync(string fileName)
	{
		var articleImgStream = new MemoryStream();
		var getObjectArgs = new GetObjectArgs()
			.WithBucket(Constants.BucketNames.ArticleImages)
			.WithObject(fileName)
			.WithCallbackStream(data => data.CopyTo(articleImgStream));
		await MinioCl.GetObjectAsync(getObjectArgs).ConfigureAwait(false);
		articleImgStream.Seek(0, SeekOrigin.Begin);  // 重置流位置

		return articleImgStream;
	}
}