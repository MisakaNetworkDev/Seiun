namespace Seiun.Entities;

public class PublicAnnouncementEntity : BaseEntity
{
	// 标题
	public required string Title { get; set; }
	// 内容
	public required string Content { get; set; }
	// 发布时间
	public required DateTime PublishTime { get; set; }
	// 发布人
	public required Guid AdminId { get; set; }
}