using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Commands.ApplyForJob;

public sealed record ApplyForJobCommand(Guid JobId) : IRequest<Result>;
