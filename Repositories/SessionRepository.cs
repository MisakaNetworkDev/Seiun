using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class SessionRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<SessionEntity>(dbContext, minioClient), ISessionRepository
{
	
}