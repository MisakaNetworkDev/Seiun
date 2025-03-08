using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class FinishedWordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
	: BaseRepository<FinishedWordRecordEntity>(dbContext, minioClient), IFinishedWordRepository
{
    
}