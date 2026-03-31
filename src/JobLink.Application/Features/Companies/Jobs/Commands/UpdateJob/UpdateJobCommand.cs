using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Commands.UpdateJob;

public sealed record UpdateJobCommand(
    Guid Id,
    string? Title,
    string? Description,
    string? Requirements,
    ExperienceLevel? ExperienceLevel,
    JobType? JobType,
    JobLocationType? LocationType,
    string? Country,
    string? City,
    string? Area,
    int? MinSalary,
    int? MaxSalary,
    DateOnly? ExpirationDate
) : IRequest<Result>;
