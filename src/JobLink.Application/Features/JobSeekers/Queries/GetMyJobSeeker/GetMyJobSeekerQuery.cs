using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Queries.GetMyJobSeeker;

public sealed record GetMyJobSeekerQuery : IRequest<Result<JobSeekerProfileDto>>;