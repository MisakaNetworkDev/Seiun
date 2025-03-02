using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Seiun.Entities;
using Seiun.Resources;
using Seiun.Utils;

namespace Seiun.Entities;

public class WordEntity : BaseEntity
{
    [Required]
    public required string WordText { get; set; }

    [MaxLength(200)]
    public string? Pronunciation { get; set; }

    [Required]
    public required string Definition { get; set; }

    public virtual ICollection<TagEntity> Tags { get; set; } = [];

    public virtual ICollection<WordDistractor> Distractors { get; set; } = [];
}

public class WordDistractor : BaseEntity
{
    [Required]
    public required string DistractorText { get; set; }  

    [Required]
    public required string Language { get; set; }  

    public virtual WordEntity Word { get; set; } = null!;
}

public class TagEntity : BaseEntity
{
    [Required]
    [MaxLength(Constants.Word.MaxTagNameLength, ErrorMessage = ErrorMessages.ValidationError.OverTagNameLength)]
    public required string Name { get; set; }
    
    public virtual ICollection<WordEntity> Words { get; set; } = [];
}
