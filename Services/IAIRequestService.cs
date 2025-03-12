using OpenAI.Chat;

namespace Seiun.Services;

public interface IAIRequestService
{	
	Task<string?> GetAIArticleAsync(List<string> words);
	Task<string?> GetAICoverAsync(string aiArticle);
}