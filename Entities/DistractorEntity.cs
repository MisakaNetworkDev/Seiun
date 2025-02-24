using Seiun.Entities;

public class Distractor : BaseEntity
{
    public required Guid WordId { get; set; }

    public required ICollection<Guid> DistractorWordIds { get; set; } = new List<Guid>();
}
