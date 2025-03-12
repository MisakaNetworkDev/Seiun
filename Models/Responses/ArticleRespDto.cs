using Seiun.Entities;
using Seiun.Resources;

namespace Seiun.Models.Responses;

# region ArticleListResponse

public class ArticleImgNameList
{
	public required List<string> ArticleImgNames { get; set; }
}

public sealed class ArticleImgNameListResp(int code, string message, ArticleImgNameList? articleImgNameList)
	: BaseRespWithData<ArticleImgNameList>(code, message, articleImgNameList)
{
	public static ArticleImgNameListResp Success(List<string> articleImgNames)
	{
		return new ArticleImgNameListResp(StatusCodes.Status200OK, SuccessMessages.Controller.Article.GetArticleImgNameListSuccess,
			new ArticleImgNameList
			{
				ArticleImgNames = articleImgNames
			});
	}

	public static ArticleImgNameListResp Fail(int code, string message)
	{
		return new ArticleImgNameListResp(code, message, null);
	}
}

/// <summary>
/// 文章列表
/// </summary>
public class ArticleList
{
	public required List<Guid> ArticleIds { get; set; }
}

/// <summary>
/// 文章列表响应
/// </summary>
public sealed class ArticleListResp(int code, string message, ArticleList? articleList)
	: BaseRespWithData<ArticleList>(code, message, articleList)
{
	public static ArticleListResp Success(List<Guid> articleIds)
	{
		return new ArticleListResp(StatusCodes.Status200OK, SuccessMessages.Controller.Article.GetArticleListSuccess,
			new ArticleList
			{
				ArticleIds = articleIds
			});
	}

	public static ArticleListResp Fail(int code, string message)
	{
		return new ArticleListResp(code, message, null);
	}
}

public class ArticleCover
{
	public required string ArticleCoverName	{ get; set; }
}

public sealed class ArticleCoverResp(int code, string message, ArticleCover? articleCover)
	: BaseRespWithData<ArticleCover>(code, message, articleCover)
{
	public static ArticleCoverResp Success(string articleCoverName)
	{
		return new ArticleCoverResp(StatusCodes.Status200OK, SuccessMessages.Controller.Article.GetArticleCoverNameSuccess,
			new ArticleCover
			{
				ArticleCoverName = articleCoverName
			});
	}

	public static ArticleCoverResp Fail(int code, string message)
	{
		return new ArticleCoverResp(code, message, null);
	}
}

# endregion

# region ArticleDetailResponse

/// <summary>
/// 文章详情
/// </summary>
public class ArticleDetail
{
	public required Guid CreatorId { get; set; }
	public required string Article { get; set; }
	public List<string>? ArticleImgURLs { get; set; }
	public required DateTime CreateTime { get; set; }
	public required int Like { get; set; }
	public required bool IsPinned { get; set; }
}

public sealed class ArticleDetailResp(int code, string message, ArticleDetail? articleDetail)
	: BaseRespWithData<ArticleDetail>(code, message, articleDetail)
{
	public static ArticleDetailResp Success(ArticleEntity articleEntity, int articleLikedCount)
	{
		var articleImgURLs = articleEntity.ImageFileNames?.Select(imgName => $"/resources/article-image/{imgName}").ToList();
		return new ArticleDetailResp(StatusCodes.Status200OK, SuccessMessages.Controller.Article.GetArticleDetailSuccess,
			new ArticleDetail
			{
				CreatorId = articleEntity.CreatorId,
				Article = articleEntity.Article,
				ArticleImgURLs = articleImgURLs,
				CreateTime = articleEntity.CreateTime,
				Like = articleLikedCount,
				IsPinned = articleEntity.IsPinned
			}
		);
	}

	public static ArticleDetailResp Fail(int code, string message)
	{
		return new ArticleDetailResp(code, message, null);
	}
}

# endregion

# region GetAIArticle

public class AIArticleDetail
{	

	public required string AIArticle { get; set; }
	public required string AICoverURL{ get; set; }
}

public sealed class AIArticleDetailResp(int code, string message, AIArticleDetail? aiArticleDetail)
	: BaseRespWithData<AIArticleDetail>(code, message, aiArticleDetail)
{
	public static AIArticleDetailResp Success(AIArticleEntity aiArticleEntity)
	{
		return new AIArticleDetailResp(StatusCodes.Status200OK, SuccessMessages.Controller.Article.GetArticleDetailSuccess,
			new AIArticleDetail
			{
				AIArticle = aiArticleEntity.Article,
				AICoverURL = aiArticleEntity.CoverURL
			}
		);
	}

	public static AIArticleDetailResp Fail(int code, string message)
	{
		return new AIArticleDetailResp(code, message, null);
	}
}

# endregion