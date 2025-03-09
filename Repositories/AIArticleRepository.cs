using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class AIArticleRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    : BaseRepository<AIArticleEntity>(dbContext, minioClient), IAIArticleRepository
{
    public async Task<AIArticleEntity?> GetByUserIdAsync(Guid userId)
    {
        return await DbContext.AIArticles
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .FirstOrDefaultAsync();
    }
}