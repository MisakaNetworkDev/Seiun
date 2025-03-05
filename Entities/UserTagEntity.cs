
namespace Seiun.Entities;
public class UserTagEntity : BaseEntity
{
    public required Guid UserId { get; set; }

    public required Guid TagId { get; set; }

    public required int DailyPlan { get; set; }

    public required int TotalDays { get; set; }

    public required int RemainingDays { get; set; }

    public required int LearnedCount { get; set; }
    
    public required DateTime SettingAt { get; set; }

    public virtual required UserEntity User { get; set; }

    public virtual required TagEntity Tag { get; set; }
}
