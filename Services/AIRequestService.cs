using OpenAI;
using System.ClientModel;
using OpenAI.Chat;
<<<<<<< HEAD
=======
using RestSharp;
using System.Text.Json;
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153

namespace Seiun.Services;

public class AIRequestService : IAIRequestService
{
	private readonly IConfigurationRoot _config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(".secret.json", optional: false, reloadOnChange: true)
        .Build();

<<<<<<< HEAD
	public async Task<ChatCompletion?> GetAIArticleAsync(List<string> words)
=======
	public async Task<string?> GetAIArticleAsync(List<string> words)
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
	{
		var apiKey = _config["OpenAI:ApiKey"];
    	var endpoint = _config["OpenAI:Endpoint"];

		string prompt = string.Join(",", words);
		var clientOptions = new OpenAIClientOptions
		{
			Endpoint = new Uri($"{endpoint}")
		};
		var clientCredentials = new ApiKeyCredential($"{apiKey}");
<<<<<<< HEAD
		var client = new OpenAIClient(clientCredentials, clientOptions).GetChatClient("deepseek-chat");
		var systemPrompt = """
							请根据以下单词，使用逗号分隔，不区分大小写，生成一篇英文文章，帮助学习这些单词。
							文章必须使用 Markdown 语法，以markdown文本返回，标题放在title字段，内容放在content字段。
=======
		var client = new OpenAIClient(clientCredentials, clientOptions).GetChatClient("deepseek-reasoner");
		var systemPrompt = """
							请根据以下单词，使用逗号分隔，不区分大小写，生成一篇英文文章，帮助学习这些单词。
							文章必须使用 Markdown 语法，以markdown文本返回。
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
							文章中也可以使用一些学习的单词的一些词性变换和语法词组，学习的单词和相关语法,词性变换，词组加粗。
							
							EXAMPLE INPUT:
							hello,world
							
<<<<<<< HEAD
							EXAMPLE JSON OUTPUT:
							{
								"title":"# Title",
								"content":"This is **bold** text."
							}
=======
							EXAMPLE OUTPUT:
							# The Beauty of the **World**\n\nIn this vast **world**, a simple **hello** can create new friendships, \n
							brighten someone's day, and bring warmth to a lonely heart. 								
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
							""";
		var userPrompt = $"{prompt}";
		var completionOptions = new ChatCompletionOptions
		{
			Temperature = 1.5f,
<<<<<<< HEAD
			ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
=======
			ResponseFormat = ChatResponseFormat.CreateTextFormat()
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
		};
		var messages = new ChatMessage[]
		{
			new SystemChatMessage(systemPrompt),
			new UserChatMessage(userPrompt)
		};
		ChatCompletion completion = await client.CompleteChatAsync(messages, completionOptions);
<<<<<<< HEAD
		return completion;
=======
		return completion?.Content[0].Text;
	}

	public async Task<string?> GetAICoverAsync(string aiArticle)
	{
		var apiKey = _config["CA:ApiKey"];
    	var endpoint = _config["CA:Endpoint"];

		aiArticle = aiArticle.Length>900?aiArticle[..900] : aiArticle;

		var client = new RestClient($"{endpoint}");
        var request = new RestRequest
        {
            Method = Method.Post
        };
        request.AddHeader("Authorization", $"Bearer {apiKey}"); 
		request.AddHeader("Content-Type", "application/json");

		var body = new
		{
			prompt = $"根据以下英文文章生成图片，要求阳光，二次元风格。文章：{aiArticle}",
			n = 1,
			model = "dall-e-2",
			size = "512x512"
		};

		request.AddJsonBody(body);

		var response = await client.ExecuteAsync(request); 
		if(response.Content != null)
		{
			using JsonDocument doc = JsonDocument.Parse(response.Content);
			JsonElement root = doc.RootElement;
			var dataArray  = root.GetProperty("data");
			if (dataArray .GetArrayLength() > 0)
			{
				JsonElement firstElement = dataArray [0];
				string aiCoverURL = firstElement.GetProperty("url").GetString() ?? string.Empty;
				return aiCoverURL;
			}
		}
		return null;
>>>>>>> b146b558ac009a67e541bad7b44028a2b3c9d153
	}
}