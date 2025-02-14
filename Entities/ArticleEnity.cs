namespace Seiun.Entities;

public class ArticleEntity: BaseEntity
{
	// 文章
	public required string Article { get; set; }
	// 图片
	public List<string>? ImageFileNames { get; set; }
	// 发布者ID
	public required Guid CreatorId { get; set; }
	// 发布时间
	public required DateTime CreateTime { get; set; }
	// 置顶
	public required bool IsPinned { get; set; }
	// 置顶时间
	public DateTime? PinTime { get; set; }
}
