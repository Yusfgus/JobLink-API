using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Experiences.Commands.AddExperience;

public sealed record AddExperienceCommand(
    string Company,
    string Position,
    string Country,
    string? Description,
    int Salary,
    DateOnly StartDate,
    DateOnly EndDate
) : IRequest<Result<Guid>>;
