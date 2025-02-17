using Seiun.Entities;
using Seiun.Resources;
using Seiun.Utils.Enums;

namespace Seiun.Models.Responses;

#region UserLoginResponse

/// <summary>
/// Token 信息
/// </summary>
public class TokenInfo
{
    public required string Token { get; set; }
    public required long ExpireAt { get; set; }
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

#region UserProfileResponse

/// <summary>
/// 用户资料
/// </summary>
public class UserProfile
{
    public required string UserName { get; set; }
    public required string NickName { get; set; }
    public required string AvatarUrl { get; set; }
    public required Gender Gender { get; set; }
}

/// <summary>
/// 用户资料响应
/// </summary>
public sealed class UserProfileResp(int code, string message, UserProfile? userProfile)
    : BaseRespWithData<UserProfile>(code, message, userProfile)
{
    public static UserProfileResp Success(UserEntity userEntity)
    {
        return new UserProfileResp(StatusCodes.Status200OK, SuccessMessages.Controller.User.GetProfileSuccess,
            new UserProfile
            {
                UserName = userEntity.UserName,
                NickName = userEntity.NickName,
                AvatarUrl = $"/resources/avatar/{userEntity.AvatarFileName}",
                Gender = userEntity.Gender
            });
    }

    public static UserLoginResp Fail(int code, string message)
    {
        return new UserLoginResp(code, message, null);
    }
}
#endregion