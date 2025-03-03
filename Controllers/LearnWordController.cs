
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Seiun.Models.Responses;
// using Seiun.Resources;
// using Seiun.Services;
// using Seiun.Utils;
// using Seiun.Utils.Enums;


// namespace Seiun.Controllers;


// [ApiController]
// [Route("/api/learn")]
// public class LearnWordController(ILogger<LearnWordController> logger, IRepositoryService repository)
//     : ControllerBase
// {
// 	[HttpPost("word", Name = "LearnWord")]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// 	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
// 	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
// 	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
//     public async Task<IActionResult> LearnWord()
//     {
//         var userId = User.GetUserId();
//         if (userId == null)
//         {
//             return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory.NewFailedBaseResponse(
//                 StatusCodes.Status400BadRequest,
//                 ErrorMessages.ValidationError.UserIdRequired
//             ));
//         }

//         var studyRecord = await repository.StudyWordRepository.GetStudyRecordByUserId(userId.Value);
//         if(studyRecord == null)
//         {
//             // 导入单词
//         }
//         else
//         {
//             switch (studyRecord.Stage)
//             {
//                 // 第一阶段
//                 case StudyStage.First:
//                     // 全部过一遍 直到队列1为空
//                     break;
//                 case StudyStage.Second:
//                     // 复习 直到队列2为空
//                     break;
//             }
//         }
//     }
    
// }