using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seiun.Models.Parameters;
using Seiun.Models.Responses;
using Seiun.Resources;
using Seiun.Services;
using Seiun.Utils;
using Seiun.Utils.Enums;

namespace Seiun.Controllers;

[ApiController]
[Route("/api/admin")]
public class AdminController(ILogger<AdminController> logger, IRepositoryService repository, IJwtService jwt)
    : ControllerBase
{
    [HttpPost("user-list", Name = "GetUserList")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = $"{nameof(UserRole.SuperAdmin)}")]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserList()
    {
        try
        {
            var users = await repository.UserRepository.GetAllAsync();
            var userList = users.Select(user => new UserList
            {
                UserId = user.Id,
                Role = user.Role,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                NickName = user.NickName
            }).ToList();

            if (userList.Count == 0)
                return Ok(UserListResp.Success(
                    ErrorMessages.Controller.User.UserNotFound,
                    []
                ));

            return Ok(UserListResp.Success(
                SuccessMessages.Controller.User.GetListSuccess,
                userList
            ));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Get user list failed");
            return StatusCode(StatusCodes.Status500InternalServerError,
                UserListResp.Fail(StatusCodes.Status500InternalServerError,
                    ErrorMessages.Controller.User.UserListFailed
                ));
        }
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="userLogin">用户登录信息DTO</param>
    /// <returns>登录结果DTO</returns>
    [HttpPost("login", Name = "AdminLogin")]
    [Authorize(Roles = $"{nameof(UserRole.SuperAdmin)}")]
    [ProducesResponseType(typeof(UserLoginResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserLoginResp), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(UserLoginResp), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var user = userLogin switch
        {
            { UserName: { } userName } when !string.IsNullOrWhiteSpace(userName) =>
                await repository.UserRepository.GetByUserNameAsync(userName),
            { PhoneNumber: { } phoneNumber } when !string.IsNullOrWhiteSpace(phoneNumber) =>
                await repository.UserRepository.GetByPhoneNumberAsync(phoneNumber),
            { Email: { } email } when !string.IsNullOrWhiteSpace(email) =>
                await repository.UserRepository.GetByEmailAsync(email),
            _ => null
        };

        if (user == null)
            return StatusCode(StatusCodes.Status403Forbidden, UserLoginResp.Fail(
                StatusCodes.Status403Forbidden,
                ErrorMessages.Controller.User.UserNotFound
            ));

        if (!PasswordUtils.VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            return StatusCode(StatusCodes.Status403Forbidden, UserLoginResp.Fail(
                StatusCodes.Status403Forbidden,
                ErrorMessages.Controller.User.UserLoginFailed
            ));

        var token = jwt.GenerateToken(user);
        var tokenInfo = new TokenInfo
        {
            Token = token,
            UserId = user.Id.ToString(),
            ExpireAt = DateTimeOffset.Now.AddHours(Constants.Token.TokenExpirationTime).ToUnixTimeSeconds()
        };
        return Ok(UserLoginResp.Success(tokenInfo));
    }
}