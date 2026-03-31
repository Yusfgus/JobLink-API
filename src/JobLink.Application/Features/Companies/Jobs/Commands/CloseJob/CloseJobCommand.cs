using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Companies.Jobs.Commands.CloseJob;

public sealed record CloseJobCommand(Guid Id) : IRequest<Result>;
