using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.UpdateExperience;

public sealed record UpdateExperienceCommand(
    Guid ExperienceId,
    string Company,
    string Position,
    string Country,
    string? Description,
    int Salary,
    DateOnly StartDate,
    DateOnly EndDate
) : IRequest<Result>;
