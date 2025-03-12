using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class SessionRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<WordSessionEntity>(dbContext, minioClient), ISessionRepository
{
	public Task<WordSessionEntity?> GetSessionByUserIdAsync(Guid userId)
	{
		return DbContext.Sessions.Where(s => s.UserId == userId).FirstOrDefaultAsync();
	}
}