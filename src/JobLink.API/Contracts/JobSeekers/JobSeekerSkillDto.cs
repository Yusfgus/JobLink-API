using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record JobSeekerSkillDto(
    Guid SkillId,
    SkillLevel Level
);
