using Seiun.Services;
using Seiun.Utils;
using Seiun.Resources;
using Seiun.Utils.Enums;
using Seiun.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Seiun.Entities;

namespace Seiun.Controllers;

[ApiController,Route("api/session")]
public class SessionController(ILogger<SessionController> logger, IRepositoryService repository, ICurrentStudySessionService currentStudySession)
	: ControllerBase
{
	/// <summary>
	/// 开始学习单词会话
	/// </summary>
	/// <returns>会话信息</returns>
	[HttpGet("start", Name = "StartStudy")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{nameof(UserRole.User)},{nameof(UserRole.Creator)}{nameof(UserRole.Admin)},{nameof(UserRole.SuperAdmin)}")]
	[ProducesResponseType(typeof(StartStudyResp), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> StartStudy()
	{
		var userId = User.GetUserId();
		if (userId == null)
		{
			return StatusCode(StatusCodes.Status403Forbidden, ResponseFactory.NewFailedBaseResponse(
				StatusCodes.Status403Forbidden,
				ErrorMessages.Controller.Any.InvalidJwtToken
			));
		}

		var selectedTag = await repository.UserTagRepository.GetStudyingTagByUserIdAsync(userId.Value);
		if(selectedTag==null)
		{
			return NotFound(StartStudyResp.Fail(
				StatusCodes.Status404NotFound,
				ErrorMessages.Controller.UserTag.UserTagNotFound
			));
		}
	    
		var session = new SessionEntity
		{
            UserId = userId.Value,
			SessionAt = DateTime.Now,
		};
		var wordQUeue = new Queue<WordEntity>();

		int reviewingWordCount = 0;
		var reviewingWords = await repository.ErrorWordRepository.GetErrorWordIdsByUserIdAsync(userId.Value);
		if(reviewingWords!=null&&reviewingWords.Count>0)
		{
			reviewingWordCount = reviewingWords.Count;
			foreach(var wordId in reviewingWords)
			{
				var word = await repository.WordRepository.GetByIdAsync(wordId);
				if(word!=null)
				{
					wordQUeue.Enqueue(word);
				}
			}
		}

		int studyingWordCount = 0;
	    var studyWords = await repository.WordRepository.GetWordsByTagAsync(selectedTag.Tag.Name, userId.Value, selectedTag.DailyPlan);
		if(studyWords!=null&&studyWords.Count>0)
		{
			studyingWordCount = studyWords.Count;
			foreach(var word in studyWords)
			{
				wordQUeue.Enqueue(word);
			}
		}

		repository.SessionRepository.Create(session);
		var NewSessionResult = currentStudySession.AddSession(session.Id, wordQUeue, logger);
		if(NewSessionResult && await repository.SessionRepository.SaveAsync())
		{
			return Ok(StartStudyResp.Success(session.Id, reviewingWordCount, studyingWordCount));
		}
		
		logger.LogWarning("Start study session failed");
		return StatusCode(StatusCodes.Status500InternalServerError, StartStudyResp.Fail(
			StatusCodes.Status500InternalServerError,
			ErrorMessages.Controller.Session.StartFailed
		));
	}

	/// <summary>
 	/// 获取下一个单词
	/// </summary>
	/// <param name="sessionId">会话ID</param>
	/// <returns>下一个单词信息</returns>
	[HttpGet("get-nextword/{sessionId:Guid}", Name = "GetNextWord")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{nameof(UserRole.User)},{nameof(UserRole.Creator)}{nameof(UserRole.Admin)},{nameof(UserRole.SuperAdmin)}")]
	[ProducesResponseType(typeof(GetNextWordResp), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(GetNextWordResp), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(GetNextWordResp), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(GetNextWordResp), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetNextWord(Guid sessionId){
        var userId = User.GetUserId();
		if (userId == null)
		{
			return StatusCode(StatusCodes.Status403Forbidden, ResponseFactory.NewFailedBaseResponse(
				StatusCodes.Status403Forbidden,
				ErrorMessages.Controller.Any.InvalidJwtToken
			));
		}

		var session = await repository.SessionRepository.GetByIdAsync(sessionId);
		if(session==null || session.UserId!=userId.Value)
		{
			return NotFound(GetNextWordResp.Fail(
				StatusCodes.Status404NotFound,
				ErrorMessages.Controller.Session.NotFoundSession
			));
		}

		try
		{
			var word = currentStudySession.GetNextWord(sessionId, logger);
			if(word != null)
			{
				return Ok(GetNextWordResp.Success(word));
			}
			else
			{
				return NotFound(GetNextWordResp.Fail(
					StatusCodes.Status404NotFound,
					ErrorMessages.Controller.Session.NotFindNextWord
				));
			}
		}
		catch (Exception e)
		{
			logger.LogWarning(e,"User {} get next word failed", userId);
			return StatusCode(StatusCodes.Status500InternalServerError, GetNextWordResp.Fail(
				StatusCodes.Status500InternalServerError,
				ErrorMessages.Controller.Session.GetNextWordFailed
			));
		}
	}	
}