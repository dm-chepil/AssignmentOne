namespace AssignmentOne.Api.DTOs;

public class VideoGameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}
