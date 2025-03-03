using Microsoft.EntityFrameworkCore; // 添加这行，确保 FirstOrDefaultAsync 可用
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class StudyWordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    : BaseRepository<StudyStageEntity>(dbContext, minioClient), IStudyWordRepository
{
    public async Task<StudyStageEntity?> GetStudyRecordByUserId(Guid userId)
    {
        return await DbContext.StudyStage.FirstOrDefaultAsync(s => s.UserId == userId);
    }
}
