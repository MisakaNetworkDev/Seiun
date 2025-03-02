// using Microsoft.AspNetCore.Mvc;
// using Seiun.Models.Responses;
// using Seiun.Resources;
// using Seiun.Services;
// namespace Seiun.Controllers;


// [ApiController, Route("/api/tag")]
// public class TagController(ILogger<UserController> logger, IRepositoryService repository)
//     : ControllerBase
// {
//     // 获取词书并制定计划
//     [HttpPost("create-tag", Name = "CreateTag")]
//     [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> CreateTag([FromBody] Guid userId, [FromBody] Guid tagId ,[FromBody] int dailyplan)
//     {
//         var user = await repository.UserRepository.GetByIdAsync(userId);
//         if (user == null)
//         {
//             return NotFound(UserProfileResp.Fail(
//                 StatusCodes.Status404NotFound,
//                 ErrorMessages.Controller.User.UserNotFound
//             ));
//         }

//         var tag = await repository.TagRepository.GetByIdAsync(tagId);
//         if (tag == null)
//         {
//             return NotFound(UserProfileResp.Fail(
//                 StatusCodes.Status404NotFound,
//                 ErrorMessages.Controller.Tag.TagNotFound
//             ));
//         }
//         var totalWords = tag.Words.Count;

//         var remainingDays = totalWords / dailyplan;
//         var userTag = new UserTagEntity{
//             UserId = userId,
//             TagId = tagId,
//             DailyPlan = dailyplan,
//             RemainingDays = remainingDays,
//             User = user,
//             Tag = tag
//         };
//         repository.U

//     }

//     // 返回已选词库列表
//     [HttpGet("get-selected-tags", Name = "GetSelectedTags")]

//     // 取消选择词库
//     [HttpDelete("cancel-tag", Name = "CancelTag")]

//     // 返回未选词库列表
//     [HttpGet("get-unselected-tags", Name = "GetUnselectedTags")]

//     /// <summary>
//     /// 选择词库
//     /// </summary>
//     /// <param name="userId">用户ID</param>
//     /// <param name="tagId">标签ID</param>
//     /// <returns>选择标签结果</returns>
//     [HttpGet("select-tag", Name = "SelectTag")]
//     [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status200OK)]
//     [ProducesResponseType(typeof(UserProfileResp), StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> SelectTag([FromBody] Guid userId, [FromBody] Guid tagId)
//     {
//         var user = await repository.UserRepository.GetByIdAsync(userId);
//         if (user == null)
//         {
//             return NotFound(UserProfileResp.Fail(
//                 StatusCodes.Status404NotFound,
//                 ErrorMessages.Controller.User.UserNotFound
//             ));
//         }
        
//     }
// }