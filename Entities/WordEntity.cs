using System.ComponentModel.DataAnnotations;
using Seiun.Entities;
using Seiun.Resources;
using Seiun.Utils;

public class Word : BaseEntity
{
    [Required]
    public required string WordText { get; set; }

    [MaxLength(200)]
    public string? Pronunciation { get; set; }

    [Required]
    public required string Definition { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

public class Tag : BaseEntity
{
    [Required]
    [MaxLength(Constants.Word.MaxTagNameLength, ErrorMessage = ErrorMessages.ValidationError.OverTagNameLength)]
    public required string Name { get; set; }

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
