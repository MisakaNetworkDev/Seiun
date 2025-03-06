using Seiun.Entities;
using Seiun.Repositories;
using Seiun.Controllers;

namespace Seiun.Services;

public class CurrentStudySessionService : ICurrentStudySessionService
{
	private readonly Dictionary<Guid,Queue<WordEntity>> _CurrentStudySessions = [];

	// 添加Session
    public void AddSession(Guid SessionId, Queue<WordEntity> Words, ILogger<SessionController> logger)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId))
		{
			logger.LogWarning("Session {} already exists",SessionId);
			return;
		}
		_CurrentStudySessions.Add(SessionId,Words);
	}

    // 获取下一个单词
	public WordEntity? GetNextWord(Guid SessionId, ILogger<SessionController> logger)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId)==false)
		{
			logger.LogWarning("Session {} does not exist",SessionId);
			return null;
		}
		return _CurrentStudySessions[SessionId].Peek();
	}

    // 插入错误单词到队尾
	public void InsertWord(Guid SessionId, ILogger<SessionController> logger)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId)==false)
		{	
			logger.LogWarning("Session {} does not exist",SessionId);
			return;
		}
		var Word = _CurrentStudySessions[SessionId].Dequeue();
		_CurrentStudySessions[SessionId].Enqueue(Word);
	}

    // 会话结束，移除Session
	public void RemoveSession(Guid SessionId, ILogger<SessionController> logger)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId)&&_CurrentStudySessions[SessionId].Count==0)
		{
			_CurrentStudySessions.Remove(SessionId);
		}
		else
		{
			logger.LogWarning("Session {} does not exist or not finished",SessionId);
		}
	}

	// 定时清理Session
	public async Task ClearSessionAsync(ISessionRepository sessionRepository, ILogger logger)
	{
		var endTime = DateTime.Now;
		foreach(var Session in _CurrentStudySessions)
		{
			var userSession = await sessionRepository.GetByIdAsync(Session.Key);
			if(userSession!=null)
			{
				TimeSpan hoursSpan = new(endTime.Ticks-userSession.SessionAt.Ticks);
				if(hoursSpan.TotalHours>20)
				{
					_CurrentStudySessions.Remove(Session.Key);
				}
			}
			else
			{
				logger.LogWarning("SessionEntity does not exist for Session {}",Session.Key);
			}
		}
	}
}