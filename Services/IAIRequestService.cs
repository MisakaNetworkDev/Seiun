using OpenAI.Chat;

namespace Seiun.Services;

public interface IAIRequestService
{	
	Task<ChatCompletion?> GetAIArticleAsync(List<string> words);
}