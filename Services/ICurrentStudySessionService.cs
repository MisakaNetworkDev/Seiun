using Seiun.Entities;

namespace Seiun.Services;

public interface ICurrentStudySessionService
{
	public void AddSession(Guid SessionId, Queue<WordEntity> Words);
	public WordEntity? GetNextWord(Guid SessionId);
}