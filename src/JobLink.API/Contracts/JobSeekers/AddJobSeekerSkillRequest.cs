using JobLink.Domain.Common.Enums;

namespace JobLink.API.Contracts.JobSeekers;

public sealed record AddJobSeekerSkillRequest(
    Guid SkillId,
    SkillLevel Level
);
