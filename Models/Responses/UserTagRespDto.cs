namespace Seiun.Models.Responses;

public class UserUnSelectTagRespDto
{
    public required string TagName { get; set; }
    public required int WordCount { get; set; }
}

public sealed class UserUnSelectTagListResp(int code, string message, List<UserUnSelectTagRespDto>? userUnselectedTagList)
    : BaseRespWithData<List<UserUnSelectTagRespDto>>(code, message, userUnselectedTagList)
{
    public static UserUnSelectTagListResp Success(string message, List<UserUnSelectTagRespDto> userUnselectedTagList)
    {
        return new UserUnSelectTagListResp(200, message, userUnselectedTagList);
    }

    public static UserUnSelectTagListResp Fail(int code, string message)
    {
        return new UserUnSelectTagListResp(code, message, null);
    }

}

public class UserSelectedTagRespDto
{
    public required string TagName { get; set; }
    public required int LearnedCount { get; set; }
    public required int TotalCount { get; set; }
}

public sealed class UserSelectedTagListResp(int code, string message, List<UserSelectedTagRespDto>? userSelectedTagList)
    : BaseRespWithData<List<UserSelectedTagRespDto>>(code, message, userSelectedTagList)
{
    public static UserSelectedTagListResp Success(string message, List<UserSelectedTagRespDto> userSeletedTagList)
    {
        return new UserSelectedTagListResp(200, message, userSeletedTagList);
    }

    public static UserSelectedTagListResp Fail(int code, string message)
    {
        return new UserSelectedTagListResp(code, message, null);
    }

}

public sealed class SeletedTagDetailResp(int code, string message, UserSelectedTagRespDto? userSelectedTag)
    : BaseRespWithData<UserSelectedTagRespDto>(code, message, userSelectedTag)
{
    public static SeletedTagDetailResp Success(string message, UserSelectedTagRespDto userSelectedTag)
    {
        return new SeletedTagDetailResp(200, message, userSelectedTag);
    }

    public static SeletedTagDetailResp Fail(int code, string message)
    {
        return new SeletedTagDetailResp(code, message, null);
    }
    
}

