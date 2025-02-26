using System.ComponentModel.DataAnnotations;
using Seiun.Utils.Enums;
using Seiun.Resources;

namespace Seiun.Entities;
public class UserWordRecord : BaseEntity
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.WordIdRequired)]
    public required Guid WordId { get; set; }

    public required WordStage Stage { get; set; }             

    public DateTime NextReviewTime { get; set; } = DateTime.Now.AddDays(1);   

    public int WrongCount { get; set; } = 0;      

    public DateTime LastStudyTime { get; set; } = DateTime.Now;

    public virtual required UserEntity User { get; set; }
    public virtual required WordEntity Word { get; set; }
}
