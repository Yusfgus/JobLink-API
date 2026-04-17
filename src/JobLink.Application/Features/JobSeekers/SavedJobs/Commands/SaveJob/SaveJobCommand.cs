using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Commands.SaveJob;

public sealed record SaveJobCommand(Guid JobId) : IRequest<Result>;
