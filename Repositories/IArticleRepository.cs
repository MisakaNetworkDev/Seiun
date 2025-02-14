using Seiun.Entities;

namespace Seiun.Repositories;

public interface IArticleRepository : IBaseRepository<ArticleEntity>
{
	Task<List<Guid>?> GetArticleListAsync(int queryLength, DateTime? from);
	Task<List<Guid>?> GetArticleListByUserIdAsync(Guid userId);
	Task<string> UploadArticleImgAsync(Stream articleimgData, string articleImgExtension);
	Task<bool> DeleteAticleImgAsync(List<string> articleImgName);
	Task<MemoryStream> GetArticleImgAsync(string fileNames);
}