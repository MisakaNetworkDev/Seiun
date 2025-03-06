using Seiun.Entities;
using Seiun.Repositories;
using Seiun.Controllers;

namespace Seiun.Services;

public interface ICurrentStudySessionService
{
	public void AddSession(Guid SessionId, Queue<WordEntity> Words, ILogger<SessionController> logger);
	public WordEntity? GetNextWord(Guid SessionId, ILogger<SessionController> logger);
	public void InsertWord(Guid SessionId, ILogger<SessionController> logger);
	public void RemoveSession(Guid SessionId, ILogger<SessionController> logger);
	public Task ClearSessionAsync(ISessionRepository sessionRepository, ILogger logger);
}