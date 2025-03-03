using Seiun.Entities;

namespace Seiun.Repositories;

public interface IStudyWordRepository : IBaseRepository<StudyStageEntity>
{
    Task<StudyStageEntity?> GetStudyRecordByUserId(Guid userId);
}