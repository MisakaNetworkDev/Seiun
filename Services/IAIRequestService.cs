using OpenAI.Chat;

namespace Seiun.Services;

public interface IAIRequestService
{	
<<<<<<< HEAD
	Task<ChatCompletion?> GetAIArticleAsync(List<string> words);
=======
	Task<string?> GetAIArticleAsync(List<string> words);
	Task<string?> GetAICoverAsync(string aiArticle);
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
}