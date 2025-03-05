using Seiun.Entities;
using Seiun.Repositories;
using System.Collections.Concurrent;

namespace Seiun.Services;

public class CurrentStudySessionService(IServiceProvider serviceProvider) : ICurrentStudySessionService
{
	private readonly ConcurrentDictionary<Guid,Queue<WordEntity>> _CurrentStudySessions = new();
	private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	private readonly Lock _lock = new();

	// 添加Session
    public void AddSession(Guid SessionId, Queue<WordEntity> Words)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId)==false)
		{
			_CurrentStudySessions.TryAdd(SessionId,Words);
		}
	}

    // 获取下一个单词
	public WordEntity? GetNextWord(Guid SessionId)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId))
		{
			return _CurrentStudySessions[SessionId].Dequeue();
		}
		else
		{
			return null;
		}
	}

    // 插入错误单词到队尾
	public void InsertWord(Guid SessionId, WordEntity Word)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId))
		{
			_CurrentStudySessions[SessionId].Enqueue(Word);
		}
	}

    // 会话结束，移除Session
	public void RemoveSession(Guid SessionId)
	{
		if(_CurrentStudySessions.ContainsKey(SessionId)&&_CurrentStudySessions[SessionId].Count==0)
		{
			_CurrentStudySessions.TryRemove(SessionId,out _ );
		}
	}

	// 定时清理Session
	public async void ClearSession()
	{
		var sessionRepository = _serviceProvider.GetRequiredService<ISessionRepository>();
		var endTime = DateTime.Now;
		foreach(var Session in _CurrentStudySessions)
		{
			var userSession = await sessionRepository.GetByIdAsync(Session.Key);
			if(userSession!=null)
			{
				TimeSpan hoursSpan = new(endTime.Ticks-userSession.SessionAt.Ticks);
				if(hoursSpan.TotalHours>20)
				{
					_CurrentStudySessions.TryRemove(Session.Key,out _ );
				}
			}
		}
	}
}