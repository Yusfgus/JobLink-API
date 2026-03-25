using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record UpdateJobSeekerSkillRequest(
    Guid SkillId,
    SkillLevel Level
);