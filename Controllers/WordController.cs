using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Seiun.Models.Responses;
using Seiun.Resources;
using Seiun.Services;
using Seiun.Entities;

namespace Seiun.Controllers;

[ApiController, Route("/api/word")]
public class WordController(ILogger<WordController> logger, IRepositoryService repository)
    : ControllerBase
{
    [HttpPost("importWordFromJson", Name = "ImportWordsFromJson")]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResp), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ImportWordsFromJson(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                ErrorMessages.Controller.Word.WordFileNotFound
            ));
        }

        if (!Path.GetExtension(file.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                ErrorMessages.Controller.Any.FileFormatNotJson
            ));
        }

        try
        {
            using var stream = file.OpenReadStream();
            var wordImportDto = await JsonSerializer.DeserializeAsync<WordImportDto>(
                stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (wordImportDto?.Words is null || wordImportDto.Words.Count == 0)
            {
                return BadRequest(ResponseFactory.NewFailedBaseResponse(
                    StatusCodes.Status400BadRequest,
                    ErrorMessages.Controller.Word.WordsNotFound
                ));
            }

            var existingWords = await repository.WordRepository.GetExistingWordsAsync(
                wordImportDto.Words.Select(dto => dto.WordText)
            );

            var existingWordSet = new HashSet<string>(existingWords);

            if (existingWordSet.Count > 0)
            {
                logger.LogInformation("Existing words: {ExistingWords}", string.Join(", ", existingWordSet));
            }

            var wordsToInsert = wordImportDto.Words
                .Where(wordDto => !existingWordSet.Contains(wordDto.WordText)) 
                .Select(wordDto => new WordEntity
                {
                    WordText = wordDto.WordText,
                    Pronunciation = wordDto.Pronunciation,
                    Definition = wordDto.Definition,
                    Tags = wordDto.Tags.Select(tagName => new TagEntity { Name = tagName }).ToList(),
                    Distractors = wordDto.Distractors.English
                        .Select(distractor => new WordDistractor { DistractorText = distractor, Language = "English" })
                        .Union(wordDto.Distractors.Chinese
                            .Select(distractor => new WordDistractor { DistractorText = distractor, Language = "Chinese" }))
                        .ToList()
                }).ToList();

            if (wordsToInsert.Count == 0)
            {
                return Ok(ResponseFactory.NewSuccessBaseResponse(ErrorMessages.Controller.Word.NoNewWords));
            }

            repository.WordRepository.BulkInsert(wordsToInsert);
            if (await repository.WordRepository.SaveAsync())
            {
                return Ok(ResponseFactory.NewSuccessBaseResponse($"成功导入 {wordsToInsert.Count} 个新单词！"));
            }

            logger.LogError("Words import failed");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status500InternalServerError,
                ErrorMessages.Controller.Word.ImportFailed
            ));
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Parse JSON data failed: {Message}", ex.Message);
            return BadRequest(ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status400BadRequest,
                ErrorMessages.Controller.Word.ParseJsonFailed
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Import words failed: {Message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory.NewFailedBaseResponse(
                StatusCodes.Status500InternalServerError,
                ErrorMessages.Controller.Word.ImportFailed
            ));
        }
    }
}
