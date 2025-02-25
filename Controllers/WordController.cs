using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Seiun.Models.Responses;
using Seiun.Resources;
using Seiun.Services;

namespace Seiun.Controllers;

[ApiController, Route("/api/word")]
public class WordController(ILogger<WordController> logger, IRepositoryService repository)
    : ControllerBase
{
    /// <summary>
    /// 导入词库（通过上传 JSON 文件）
    /// </summary>
    /// <param name="file">上传的 JSON 文件</param>
    /// <returns>导入结果</returns>
    [HttpPost("import", Name = "ImportWordsFromJson")]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ImportWordsFromJson(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                ErrorMessages.Controller.Word.WordFileNotFound
            ));
        }

        if (!Path.GetExtension(file.FileName).Equals(".json", StringComparison.CurrentCultureIgnoreCase))
        {
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                ErrorMessages.Controller.Any.FileFormatNotJson
            ));
        }

        try
        {
            // 解析 JSON 文件
            using var stream = new StreamReader(file.OpenReadStream());
            var jsonContent = await stream.ReadToEndAsync();
            var wordImportDto = JsonSerializer.Deserialize<WordImportDto>(jsonContent);

            if (wordImportDto?.Words == null || !wordImportDto.Words.Any())
            {
                return BadRequest(ResponseFactory.NewFailedBaseResponse(
                    StatusCodes.Status400BadRequest,
                    "词库数据不能为空"
                ));
            }

            // 保存每个单词到数据库
            foreach (var wordDto in wordImportDto.Words)
            {
                var word = new WordEntity
                {
                    WordText = wordDto.WordText,
                    Pronunciation = wordDto.Pronunciation,
                    Definition = wordDto.Definition,
                    Tags = wordDto.Tags.Select(tagName => new Tag { Name = tagName }).ToList()
                };

                repository.WordRepository.Create(word);
            }

            if (await repository.WordRepository.SaveAsync())
            {
                return Ok(ResponseFactory.NewSuccessBaseResponse("词库导入成功"));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status500InternalServerError,
                "词库导入失败"
            ));
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "JSON 解析失败");
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                "JSON 格式不正确"
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "导入词库失败");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status500InternalServerError,
                "导入过程中发生错误"
            ));
        }
    }
}