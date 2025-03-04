using System.ComponentModel.DataAnnotations;
using Seiun.Resources;

namespace Seiun.Entities;
public class SessionEntity : BaseEntity
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid UserId { get; set; }
	
    public required DateTime SessionAt { get; set; }
}
