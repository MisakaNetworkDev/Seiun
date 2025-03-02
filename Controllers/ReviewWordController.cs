using Microsoft.AspNetCore.Mvc;
using Seiun.Models.Responses;
using Seiun.Utils.Enums;
using Seiun.Resources;
using Seiun.Services;
using Seiun.Utils;
using Seiun.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nest;

namespace Seiun.Controllers;
/// <summary>
/// 复习单词控制器
/// </summary>
/// <param name="logger">日志</param>
/// <param name="repository">仓库服务</param>
[ApiController,Route("api/reviewing")]
public class ReviewWordController(ILogger<ReviewWordController> logger, IRepositoryService repository) : ControllerBase{

	/// <summary>
	/// 创建复习单词
	/// </summary>
	/// <param name="wordIDs">单词ID列表</param>
	/// <returns>创建成功</returns>
	[HttpPost("Create", Name = "CreateReviewingWord")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{nameof(UserRole.User)},{nameof(UserRole.Creator)},{nameof(UserRole.Admin)},{nameof(UserRole.SuperAdmin)}")]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateReviewingWord([FromBody] List<Guid> wordIDs)
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

			repository.UserWordRepository.BulkUpdate(existingRecords);
		}

		if(wordIDs != null && wordIDs.Count > 0)
		{
			var newRecords = new List<UserWordRecordEntity>();
			foreach (var id in wordIDs)
			{
				var word = await repository.WordRepository.GetByIdAsync(id);
				if(word == null)
				{
					return BadRequest(ResponseFactory.NewFailedBaseResponse(
						StatusCodes.Status400BadRequest,
						ErrorMessages.ValidationError.WordIdError
					));
				}

				newRecords.Add(new UserWordRecordEntity
				{
					WordId = id,
					UserId = userId.Value,
					Stage = WordStage.Reviewing,
					NextReviewTime = DateTime.Now.AddDays(1),
					WrongCount = 1,
					LastStudyTime = DateTime.Now,
					Word = word
				});
			}

			repository.UserWordRepository.BulkCreate(newRecords);
		}

		if(await repository.UserWordRepository.SaveAsync())
		{
			return Ok(ResponseFactory.NewSuccessBaseResponse(SuccessMessages.Controller.UserWordRecord.CreateSuccess));
		}

		logger.LogError("User {} failed to create reviewed word", userId);
		return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
			StatusCodes.Status500InternalServerError,
			ErrorMessages.Controller.UserWordRecord.CreateFailed
		));
	}

	/// <summary>
	/// 更新复习单词
	/// </summary>
	/// <param name="wordIDs">单词ID列表</param>
	/// <returns>更新结果</returns>
	[HttpPatch("update-reviewing", Name = "UpdateReviewingWord")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]	
	[Authorize(Roles = $"{nameof(UserRole.User)},{nameof(UserRole.Creator)},{nameof(UserRole.Admin)},{nameof(UserRole.SuperAdmin)}")]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateReviewingWord([FromBody] List<Guid> wordIDs)
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

		var records = await repository.UserWordRepository.BulkGetByIDAsync(wordIDs, userId.Value);
		if(records == null || records.Count == 0)
		{
			return NotFound(ResponseFactory.NewFailedBaseResponse(
				StatusCodes.Status404NotFound,
				ErrorMessages.Controller.UserWordRecord.NotFoundRecords
			));
		}

		records.ForEach(r => {
			r.NextReviewTime = DateTime.Now.AddDays(1);
			r.LastStudyTime = DateTime.Now;
			r.WrongCount--;
			if(r.WrongCount <= 0)
			{
				r.Stage = WordStage.Mastered;
				r.WrongCount = 0;
			} else {
				r.Stage = WordStage.NewlyLearned;
			}
		});

		repository.UserWordRepository.BulkUpdate(records);
		if(await repository.UserWordRepository.SaveAsync())
		{
			return Ok(ResponseFactory.NewSuccessBaseResponse(SuccessMessages.Controller.UserWordRecord.UpdateSuccess));
		}

		logger.LogError("User {} failed to update reviewing word", userId);
		return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
			StatusCodes.Status500InternalServerError,
			ErrorMessages.Controller.UserWordRecord.UpdateFailed
		));
	}

	/// <summary>
	/// 获取复习单词列表
	/// </summary>
	/// <returns>单词列表</returns>
	[HttpGet("list", Name = "GetReviewingWordList")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{nameof(UserRole.User)},{nameof(UserRole.Creator)},{nameof(UserRole.Admin)},{nameof(UserRole.SuperAdmin)}")]
	[ProducesResponseType(typeof(ReviewingWordListResp), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetReviewingWordListAsync()
	{
		var userId = User.GetUserId();
		if(userId == null)
		{
			return StatusCode(StatusCodes.Status403Forbidden, ReviewingWordListResp.Fail(
				StatusCodes.Status403Forbidden,
				ErrorMessages.Controller.Any.InvalidJwtToken
			));
		}

		var wordIds = await repository.UserWordRepository.GetUserWordRecordAsync(userId.Value);
		if(wordIds == null || wordIds.Count == 0)
		{
			return NotFound(ReviewingWordListResp.Fail(
				StatusCodes.Status404NotFound,
				ErrorMessages.Controller.UserWordRecord.NotFoundRecords
			));
		}

		var words = await repository.WordRepository.GetByGuidsAsync(wordIds);
		if(words.Any())
		{	
			return Ok(ReviewingWordListResp.Success(words));
		}

		logger.LogError("User {} failed to get reviewing word list", userId);
		return StatusCode(StatusCodes.Status500InternalServerError, ReviewingWordListResp.Fail(
			StatusCodes.Status500InternalServerError,
			ErrorMessages.Controller.Word.GetReviewingWordFailed
		));
	}
}