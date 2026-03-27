namespace JobLink.Application.Features.JobSeekers.DTOs;

public class ExperienceDto
{
    public string Id { get; set; } = default!;
    public string Company { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string? Description { get; set; }
    public int Salary { get; set; }
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
}
