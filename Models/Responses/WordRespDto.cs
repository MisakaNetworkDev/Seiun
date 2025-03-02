using Seiun.Entities;
using Seiun.Resources;

namespace Seiun.Models.Responses;

# region ReviewingWordListResponse

public class ReviewingWords
{
	public required IEnumerable<WordEntity> Words { get; set; }
}

public sealed class ReviewingWordListResp(int code, string message, ReviewingWords? reviewingWords)
	: BaseRespWithData<ReviewingWords>(code, message, reviewingWords)
{
	public static ReviewingWordListResp Success(IEnumerable<WordEntity> reviewingWords)
	{
		return new ReviewingWordListResp(StatusCodes.Status200OK, SuccessMessages.Controller.Word.GetReviewingWordSuccess, 
		new ReviewingWords
		{
			Words = reviewingWords
		});
	}

	public static ReviewingWordListResp Fail(int code, string message)
	{
		return new ReviewingWordListResp(code, message, null);
	}
}

# endregion