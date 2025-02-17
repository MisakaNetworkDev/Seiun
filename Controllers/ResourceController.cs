using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Minio.Exceptions;
using Seiun.Services;
using Seiun.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Seiun.Controllers;

[ApiController, Route("/resources")]
public class ResourceController(ILogger<UserController> logger, IRepositoryService repository, IJwtService jwt)
    : ControllerBase
{
    /// <summary>
    /// 头像文件接口
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="size">尺寸参数</param>
    /// <returns>头像文件</returns>
    [HttpGet("avatar/{fileName}")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvatar(string fileName, [FromQuery] int size = 256)
    {
        if (string.IsNullOrWhiteSpace(fileName) || size <= 0)
        {
            return BadRequest();
        }

        MemoryStream avatarStream;
        try
        {
            avatarStream = await repository.UserRepository.GetAvatarAsync(fileName);
        }
        catch (MinioException e)
        {
            if (e is ObjectNotFoundException)
            {
                return NotFound();
            }

            logger.LogError(e, "Failed to get avatar: {}", fileName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // 如果不需要调整大小，直接返回原图
        if (size >= Constants.User.AvatarStorageSize)
        {
            return File(avatarStream, MediaTypeNames.Image.Webp);
        }
        
        try
        {
            // 调整图像大小
            using var image = await Image.LoadAsync(avatarStream);
            image.Mutate(ipc => ipc.Resize(size, size));
            
            // 保存为 webp 格式
            var ms = new MemoryStream();
            await image.SaveAsWebpAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            
            // 返回调整后的图像
            return File(ms, MediaTypeNames.Image.Webp);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to resize avatar: {}", fileName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}