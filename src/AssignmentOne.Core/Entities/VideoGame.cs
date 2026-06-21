namespace AssignmentOne.Core.Entities;

public class VideoGame
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}
