
namespace Seiun.Entities;

public class AIArticleEntity : BaseEntity
{
<<<<<<< HEAD
	public required Guid UserId { get; set; }
	public required string Title { get; set; }
	public required string Content { get; set; }
	public required Guid SessionId { get; set; }
=======
	public required Guid UserId { get; set; }	
	public required string Article {get; set; }
	public required Guid SessionId { get; set; }
	public required string CoverURL { get; set; }
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
	public required DateTime CreatedAt { get; set; }
}