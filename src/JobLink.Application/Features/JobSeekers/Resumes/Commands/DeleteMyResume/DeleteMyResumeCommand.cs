using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Commands.DeleteMyResume;

public record DeleteMyResumeCommand : IRequest<Result>;
