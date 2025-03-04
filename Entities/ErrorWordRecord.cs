using System.ComponentModel.DataAnnotations;
using Seiun.Resources;

namespace Seiun.Entities;
public class ErrorWordRecordEntity : BaseEntity
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.WordIdRequired)]
    public required Guid WordId { get; set; }
    public required Guid SessionId { get; set; }
}
