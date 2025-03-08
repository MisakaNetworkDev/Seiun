using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class SessionRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<SessionEntity>(dbContext, minioClient), ISessionRepository
{
	public Task<SessionEntity?> GetSessionByUserIdAsync(Guid userId)
	{
		return DbContext.Sessions.Where(s => s.UserId == userId).FirstOrDefaultAsync();
	}
}