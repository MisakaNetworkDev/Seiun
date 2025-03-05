using System.ComponentModel.DataAnnotations;
using Seiun.Resources;

namespace Seiun.Entities;
public class ErrorWordRecordEntity : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid WordId { get; set; }
    public required Guid SessionId { get; set; }
}
