using System.ComponentModel.DataAnnotations;
using Seiun.Resources;
using Seiun.Utils.Enums;

namespace Seiun.Entities;

public class CommentEntity : BaseEntity
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.ContentRequired)]
    [MaxLength(500, ErrorMessage = ErrorMessages.ValidationError.OverContentLength)]
    public required string Content { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.PostIdRequired)]
    public required Guid PostId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = ErrorMessages.ValidationError.InvalidLikeCount)]
    public int LikeCount { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = ErrorMessages.ValidationError.InvalidDisLikeCount)]
    public int DislikeCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class CommentLikeEntity : BaseEntity
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.UserIdRequired)]
    public required Guid UserId { get; set; } 

    [Required(ErrorMessage = ErrorMessages.ValidationError.CommentIdRequired)]
    public required Guid CommentId { get; set; } 

    [Required(ErrorMessage = ErrorMessages.ValidationError.ActionTypeRequired)]
    public ActionType Action { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}