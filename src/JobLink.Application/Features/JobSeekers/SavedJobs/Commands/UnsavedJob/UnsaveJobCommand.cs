using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.SavedJobs.Commands.UnsaveJob;

public sealed record UnsaveJobCommand(Guid Id) : IRequest<Result>;
