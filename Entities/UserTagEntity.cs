using System.ComponentModel.DataAnnotations;
using Seiun.Entities;

public class UserTag : BaseEntity
{
    [Required]
    public required Guid UserId { get; set; }

    [Required]
    public required Guid TagId { get; set; }

    public virtual required UserEntity User { get; set; }
    public virtual required Tag Tag { get; set; }
}
