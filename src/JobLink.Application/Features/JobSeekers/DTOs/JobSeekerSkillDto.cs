namespace JobLink.Application.Features.JobSeekers.DTOs;

public sealed record JobSeekerSkillDto(
    string Id,
    string SkillId,
    string Name,
    string SkillLevel
);
