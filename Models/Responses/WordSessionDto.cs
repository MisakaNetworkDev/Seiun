using Seiun.Entities;
using Seiun.Resources;

namespace Seiun.Models.Responses;

# region StartStudyResponse

public class WordSessionDetail
{
	public required Guid WordSessionId { get; set; }
	public required int ReviewingWordCount { get; set; }
	public required int StudyingWordCount { get; set; }
}

public sealed class StartStudyResp(int code, string message, WordSessionDetail? WordsessionDetail)
	: BaseRespWithData<WordSessionDetail>(code, message, WordsessionDetail)
{
	public static StartStudyResp Success(Guid sessionId, int reviewingWordCount, int studyingWordCount)
	{
		return new StartStudyResp(StatusCodes.Status200OK, SuccessMessages.Controller.StudySession.GetSessionDetailSuccess,
			new WordSessionDetail
			{
				WordSessionId = sessionId,
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
	public required WordEntity Options { get; set; }
}

public sealed class GetNextWordResp(int code, string message, NextWordDetail? nextWordDetail)
	: BaseRespWithData<NextWordDetail>(code, message, nextWordDetail)
{
	public static GetNextWordResp Success(WordEntity nextWord)
	{
		return new GetNextWordResp(StatusCodes.Status200OK, SuccessMessages.Controller.StudySession.GetNextWordSuccess,
			new NextWordDetail
			{
				Options = nextWord
			});
	}

	public static GetNextWordResp Fail(int code, string message)
	{
		return new GetNextWordResp(code, message, null);
	}
}

# endregion