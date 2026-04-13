using JobLink.Application.Features.JobSeekers.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Queries.GetMyResume;

public record GetMyResumeQuery : IRequest<Result<ResumeDto>>;
