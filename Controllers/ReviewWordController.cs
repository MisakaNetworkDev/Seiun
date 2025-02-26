
using Microsoft.AspNetCore.Mvc;
using Seiun.Models.Responses;
using Seiun.Utils.Enums;
using Seiun.Resources;
using Seiun.Services;
using Seiun.Utils;
using Nest;
using Seiun.Entities;

namespace Seiun.Controllers;
/// <summary>
/// 复习单词控制器
/// </summary>
/// <param name="logger">日志</param>
/// <param name="repository">仓库服务</param>
public class ReviewWordController(ILogger<ReviewWordController> logger, IRepositoryService repository) : ControllerBase{

	public async Task<IActionResult> CreateReviewedWord([FromBody] List<Guid> wordIDs)
	{
		var userId = User.GetUserId();
		if(userId == null)
		{
			return StatusCode(StatusCodes.Status403Forbidden, ResponseFactory.NewFailedBaseResponse(
				StatusCodes.Status403Forbidden,
				ErrorMessages.Controller.Any.InvalidJwtToken
			));
		}

		if(wordIDs == null || wordIDs.Count == 0)
		{
			return BadRequest(ResponseFactory.NewFailedBaseResponse(
				StatusCodes.Status400BadRequest,
				ErrorMessages.ValidationError.WordIdRequired
			));
		}

		var existingRecords = await repository.UserWordRepository.BulkGetByIDAsync(wordIDs, userId.Value);
		if(existingRecords != null && existingRecords.Count > 0)
		{
			existingRecords.ForEach(r => {
				r.Stage = WordStage.Reviewing;
				r.NextReviewTime = DateTime.Now.AddDays(1);
				r.WrongCount++;
				r.LastStudyTime = DateTime.Now;
				wordIDs.Remove(r.Id);
			});
		}

		if(wordIDs != null && wordIDs.Count > 0)
		{
			var newRecords = wordIDs.Select(id => new UserWordRecordEntity
			{
				WordId = id,
				UserId = userId.Value,
				Stage = WordStage.Reviewing,
				NextReviewTime = DateTime.Now.AddDays(1),
				WrongCount = 1,
				LastStudyTime = DateTime.Now
			});

			await repository.UserWordRepository.BulkCreate();
		}
	}
}