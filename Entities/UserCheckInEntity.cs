namespace Seiun.Entities;

public class UserCheckInEntity : BaseEntity
{
    public required Guid UserId { get; set; }

    public required DateTime CheckInDate { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}
