using Seiun.Services;
using Microsoft.AspNetCore.Mvc;

namespace Seiun.Controllers;

[ApiController,Route("api/session")]
public class SessionController(ILogger<SessionController> logger, IRepositoryService repository, ICurrentStudySessionService currentStudySession) : ControllerBase
{
	
}