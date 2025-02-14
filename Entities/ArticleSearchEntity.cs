namespace Seiun.Entities;

public class ArticleSearchEntity
{
	public required string Article {get; set;}

	public required string CreatorUserName {get; set;}

	public required string CreatorNickName {get; set;}

	public required DateTime CreateTime {get; set;}
	
	public required Guid ArticleId {get; set;}
}