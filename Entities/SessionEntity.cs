

namespace Seiun.Entities;
public class SessionEntity : BaseEntity
{
    // 主键就是SessionId
    public required Guid UserId { get; set; }
    public required DateTime SessionAt { get; set; }
}
