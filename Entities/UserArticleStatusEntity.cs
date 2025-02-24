namespace Seiun.Entities;

public class UserArticleStatusEntity: BaseEntity
{
	// 用户ID
	public required Guid UserId { get; set; }
	// 文章ID
	public required Guid LikedArticleId {get; set; }
	// 点赞时间
	public required DateTime LikedTime {get; set; }
}
