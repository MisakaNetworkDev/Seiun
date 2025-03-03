using Seiun.Utils.Enums;

namespace Seiun.Entities;

public class StudyStageEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public StudyStage Stage { get; set; }
    public DateTime LastUpdated { get; set; }
    public virtual List<WordEntity> StudyCollection { get; set; } = [];
    public virtual List<WordEntity> ReviewCollection { get; set; } = [];
    public virtual List<WordEntity> NewWordCollection { get; set; } = [];

}
