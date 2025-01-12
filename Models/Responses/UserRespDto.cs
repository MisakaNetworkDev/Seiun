namespace Seiun.Models.Responses;

#region UserLoginResponse

/// <summary>
/// Token 信息
/// </summary>
public class TokenInfo
{
    public required string Token { get; set; }
    public required DateTimeOffset ExpireAt { get; set; }
}

/// <summary>
/// 用户登录响应
/// </summary>
public sealed class UserLoginResp(int code, string message, TokenInfo? tokenInfo)
    : BaseRespWithData<TokenInfo>(code, message, tokenInfo)
{
    public static UserLoginResp Success(string message, TokenInfo tokenInfo)
    {
        return new UserLoginResp(200, message, tokenInfo);
    }

    public static UserLoginResp Fail(int code, string message)
    {
        return new UserLoginResp(code, message, null);
    }
}

#endregion
