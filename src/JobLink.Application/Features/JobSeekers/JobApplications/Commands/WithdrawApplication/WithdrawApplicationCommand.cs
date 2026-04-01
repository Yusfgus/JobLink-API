using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.JobApplications.Commands.WithdrawApplication;

public sealed record WithdrawApplicationCommand(Guid Id) : IRequest<Result>;
