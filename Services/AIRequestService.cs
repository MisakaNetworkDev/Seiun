using OpenAI;
using System.ClientModel;
using OpenAI.Chat;

namespace Seiun.Services;

public class AIRequestService : IAIRequestService
{
	private readonly IConfigurationRoot _config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(".secret.json", optional: false, reloadOnChange: true)
        .Build();

	public async Task<ChatCompletion?> GetAIArticleAsync(List<string> words)
	{
		var apiKey = _config["OpenAI:ApiKey"];
    	var endpoint = _config["OpenAI:Endpoint"];

		string prompt = string.Join(",", words);
		var clientOptions = new OpenAIClientOptions
		{
			Endpoint = new Uri($"{endpoint}")
		};
		var clientCredentials = new ApiKeyCredential($"{apiKey}");
		var client = new OpenAIClient(clientCredentials, clientOptions).GetChatClient("deepseek-chat");
		var systemPrompt = """
							请根据以下单词，使用逗号分隔，不区分大小写，生成一篇英文文章，帮助学习这些单词。
							文章必须使用 Markdown 语法，以markdown文本返回，标题放在title字段，内容放在content字段。
							文章中也可以使用一些学习的单词的一些词性变换和语法词组，学习的单词和相关语法,词性变换，词组加粗。
							
							EXAMPLE INPUT:
							hello,world
							
							EXAMPLE JSON OUTPUT:
							{
								"title":"# Title",
								"content":"This is **bold** text."
							}
							""";
		var userPrompt = $"{prompt}";
		var completionOptions = new ChatCompletionOptions
		{
			Temperature = 1.5f,
			ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
		};
		var messages = new ChatMessage[]
		{
			new SystemChatMessage(systemPrompt),
			new UserChatMessage(userPrompt)
		};
		ChatCompletion completion = await client.CompleteChatAsync(messages, completionOptions);
		return completion;
	}
}