namespace Seiun.Models.Responses;

public class UserTagRespDto
{
    public Guid UserId { get; set; }
    public Guid TagId { get; set; }
    public required string TagName { get; set; }
    public required int WordCount { get; set; }
}

public sealed class UserTagListResp(int code, string message, List<UserTagRespDto>? usertagList)
    : BaseRespWithData<List<UserTagRespDto>>(code, message, usertagList)
{
    public static UserTagListResp Success(string message, List<UserTagRespDto> usertagList)
    {
        return new UserTagListResp(200, message, usertagList);
    }

    public static UserTagListResp Fail(int code, string message)
    {
        return new UserTagListResp(code, message, null);
    }
}
