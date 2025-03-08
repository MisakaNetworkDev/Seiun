using Seiun.Entities;
using Seiun.Resources;

namespace Seiun.Models.Responses;

# region StartStudyResponse

public class SessionDetail
{
	public required Guid SessionId { get; set; }
	public required int ReviewingWordCount { get; set; }
	public required int StudyingWordCount { get; set; }
}

public sealed class StartStudyResp(int code, string message, SessionDetail? sessionDetail)
	: BaseRespWithData<SessionDetail>(code, message, sessionDetail)
{
	public static StartStudyResp Success(Guid sessionId, int reviewingWordCount, int studyingWordCount)
	{
		return new StartStudyResp(StatusCodes.Status200OK, SuccessMessages.Controller.StudySession.GetSessionDetailSuccess,
			new SessionDetail
			{
				SessionId = sessionId,
				ReviewingWordCount = reviewingWordCount,
				StudyingWordCount = studyingWordCount
			});
	}

	public static StartStudyResp Fail(int code, string message)
	{
		return new StartStudyResp(code, message, null);
	}
}

# endregion

# region GetNextWordResponse

public class NextWordDetail
{
	public required string WordText { get; set; }
    public string? Pronunciation { get; set; }	
	public required string Definition { get; set; }
	public required List<string> Tags { get; set; }
    public required List<WordEntity> Distractors { get; set; }
}

public sealed class GetNextWordResp(int code, string message, NextWordDetail? nextWordDetail)
	: BaseRespWithData<NextWordDetail>(code, message, nextWordDetail)
{
	public static GetNextWordResp Success(WordEntity nextWord)
	{
		return new GetNextWordResp(StatusCodes.Status200OK, SuccessMessages.Controller.StudySession.GetNextWordSuccess,
			new NextWordDetail
			{
				WordText = nextWord.WordText,
				Pronunciation = nextWord.Pronunciation,
				Definition = nextWord.Definition,
				Tags = [.. nextWord.Tags.Select(t => t.Name)],
				Distractors = [.. nextWord.Distractors.Select(t => t.DistractedWord)]
			});
	}

	public static GetNextWordResp Fail(int code, string message)
	{
		return new GetNextWordResp(code, message, null);
	}
}

# endregion