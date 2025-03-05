using System.ComponentModel.DataAnnotations;
using Seiun.Resources;

namespace Seiun.Entities;
public class FinishedWordRecordEntity : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid WordId { get; set; }
    public required Guid SessionId { get; set; }
    public required DateTime FinishedAt { get; set; }

}
