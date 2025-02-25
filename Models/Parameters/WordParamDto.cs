using System.ComponentModel.DataAnnotations;
using Seiun.Resources;

public class WordImportDto
{
    public required List<WordDto> Words { get; set; }
}

public class WordDto
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.WordTextRequired)]
    [MaxLength(500, ErrorMessage = ErrorMessages.ValidationError.OverWordTextLength)]
    public required string WordText { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.PronunciationRequired)]
    [MaxLength(500, ErrorMessage = ErrorMessages.ValidationError.OverPronunciationLength)]
    public required string Pronunciation { get; set; }

    [Required(ErrorMessage = ErrorMessages.ValidationError.WordDefinitionRequired)]
    [MaxLength(500, ErrorMessage = ErrorMessages.ValidationError.OverDefinitionLength)]
    public required string Definition { get; set; }
    public required List<string> Tags { get; set; }
}
