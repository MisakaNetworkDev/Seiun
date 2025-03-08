using System.ComponentModel.DataAnnotations;
using Seiun.Resources;


public class WordResultDto
{
    [Required(ErrorMessage = ErrorMessages.ValidationError.WordIdRequired)]
    public required Guid WordId;
    [Required(ErrorMessage = ErrorMessages.ValidationError.SessionIdRequired)]
    public required Guid SessionId;
}