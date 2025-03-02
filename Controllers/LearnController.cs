using Microsoft.AspNetCore.Mvc;
using Seiun.Services;


namespace Seiun.Controllers;

/// <summary>
/// 评论相关接口
/// </summary>
/// <param name="logger">日志</param>
/// <param name="repository">仓库服务</param>
[ApiController]
[Route("/api/learn")]
public class LearnController(ILogger<LearnController> logger, IRepositoryService repository)
    : ControllerBase
{
    
}