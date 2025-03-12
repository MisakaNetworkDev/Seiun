
namespace Seiun.Entities;

public class AIArticleEntity : BaseEntity
{
	public required Guid UserId { get; set; }	
	public required string Article {get; set; }
	public required Guid SessionId { get; set; }
	public required string CoverURL { get; set; }
	public required DateTime CreatedAt { get; set; }
}