using System.ComponentModel.DataAnnotations;
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
    public required Guid DistractorId { get; set; }

    public virtual WordEntity DistractedWord { get; set; } = null!;
}

public class TagEntity : BaseEntity
{
    [Required]
    [MaxLength(Constants.Word.MaxTagNameLength, ErrorMessage = ErrorMessages.ValidationError.OverTagNameLength)]
    public required string Name { get; set; }
    
    public virtual ICollection<WordEntity> Words { get; set; } = [];
}

