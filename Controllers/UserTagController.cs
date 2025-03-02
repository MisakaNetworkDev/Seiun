using Microsoft.AspNetCore.Mvc;
using Seiun.Entities;
using Seiun.Models.Responses;
using Seiun.Resources;
using Seiun.Services;
namespace Seiun.Controllers;


[ApiController, Route("/api/tag")]
public class TagController(ILogger<UserController> logger, IRepositoryService repository)
    : ControllerBase
{
    
    /// <summary>
    /// 选择词库
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tagId"></param>
    /// <param name="dailyplan"></param>
    /// <returns>操作结果</returns>
    [HttpPost("select-tag", Name = "SelectTag")]
    [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SelectTag([FromBody] Guid userId, [FromBody] Guid tagId ,[FromBody] int dailyplan)
    {
        var user = await repository.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound(UserProfileResp.Fail(
                StatusCodes.Status404NotFound,
                ErrorMessages.Controller.User.UserNotFound
            ));
        }

        var tag = await repository.TagRepository.GetByIdAsync(tagId);
        if (tag == null)
        {
            return NotFound(UserProfileResp.Fail(
                StatusCodes.Status404NotFound,
                ErrorMessages.Controller.Tag.TagNotFound
            ));
        }
        var totalWords = tag.Words.Count;

        var remainingDays = totalWords / dailyplan;
        var userTag = new UserTagEntity{
            UserId = userId,
            TagId = tagId,
            DailyPlan = dailyplan,
            RemainingDays = remainingDays,
            User = user,
            Tag = tag
        };
        repository.UserTagRepository.Create(userTag);
        if(await repository.UserTagRepository.SaveAsync())
        {
            return Ok(ResponseFactory.NewSuccessBaseResponse(SuccessMessages.Controller.UserTag.CreateSuccess));
        }
        logger.LogError("Failed to create user tag");
        return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
            StatusCodes.Status500InternalServerError,
            ErrorMessages.Controller.UserTag.CreateFailed
        ));
    }

    /// <summary>
    /// 获取未选择的词库
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>未选择的词库列表</returns>
    [HttpGet("get-unselected-tags", Name = "GetUnselectedTags")]
    [ProducesResponseType(typeof(List<UserTagRespDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUnselectedTags([FromQuery] Guid userId)
    {
        var user = await repository.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound(UserProfileResp.Fail(
                StatusCodes.Status404NotFound,
                ErrorMessages.Controller.User.UserNotFound
            ));
        }

        var unselectedTags = await repository.UserTagRepository.GetUnselectedTagsAsync(userId);
        var unselectedTagDtos = unselectedTags.Select(tag => new UserTagRespDto
        {
            UserId = userId,  
            TagId = tag.Id,
            TagName = tag.Name,
            WordCount = tag.Words.Count
        }).ToList();

        return Ok(UserTagListResp.Success(SuccessMessages.Controller.Comment.GetListSuccess, unselectedTagDtos));
    }


    /// <summary>
    /// 获取已选择的词库
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>已选择的词库列表</returns>
    [HttpGet("get-selected-tags", Name = "GetSelectedTags")]
    [ProducesResponseType(typeof(List<UserTagRespDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSelectedTags([FromQuery] Guid userId)
    {
        var user = await repository.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound(UserProfileResp.Fail(
                StatusCodes.Status404NotFound,
                ErrorMessages.Controller.User.UserNotFound
            ));
        }
        var selectedTags = await repository.UserTagRepository.GetSelectedTagsAsync(userId);
        var selectedTagDtos = selectedTags.Select(tag => new UserTagRespDto
        {
            UserId = userId,
            TagId = tag.Id,
            TagName = tag.Name,
            WordCount = tag.Words.Count
        }).ToList();
        return Ok(UserTagListResp.Success(SuccessMessages.Controller.Comment.GetListSuccess, selectedTagDtos));
    }

    /// <summary>
    /// 取消用户词库
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tagId"></param>
    /// <returns>操作结果</returns>
    [HttpDelete("delete-tag", Name = "DeleteUerTag")]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteUerTag([FromQuery] Guid userId, [FromQuery] Guid tagId)
    {
        var user = await repository.UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound(UserProfileResp.Fail(
                StatusCodes.Status404NotFound,
                ErrorMessages.Controller.User.UserNotFound
            ));
        }
        await repository.UserTagRepository.CancelTagAsync(userId, tagId);
        if(await repository.UserTagRepository.SaveAsync())
        {
           return Ok(SuccessMessages.Controller.UserTag.DeleteSuccess);
        }
        logger.LogError("Failed to delete user tag {userId} {tagId}", userId, tagId);
        return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
            StatusCodes.Status500InternalServerError,
            ErrorMessages.Controller.UserTag.DeleteFailed
        ));
    }
}