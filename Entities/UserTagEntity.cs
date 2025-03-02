using System.ComponentModel.DataAnnotations;

namespace Seiun.Entities;
public class UserTagEntity : BaseEntity
{
    [Required]
    public required Guid UserId { get; set; }

    [Required]
    public required Guid TagId { get; set; }

    [Required]
    public required int DailyPlan { get; set; }

    [Required]
    public required int RemainingDays { get; set; }

    public virtual required UserEntity User { get; set; }
    public virtual required TagEntity Tag { get; set; }
}
