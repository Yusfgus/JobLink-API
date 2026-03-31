using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CreateJob;

public sealed record CreateJobCommand(
    string Title,
    string Description,
    string? Requirements,
    ExperienceLevel ExperienceLevel,
    JobType JobType,
    JobLocationType LocationType,
    Address Location,
    int MinSalary,
    int MaxSalary,
    DateOnly ExpirationDate
) : IRequest<Result<Guid>>;
