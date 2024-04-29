namespace Domain.Entities;

public class Material : BaseEntity
{
    public int CourseId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ContentUrl { get; set; }
    public Course? Course { get; set; }
}
