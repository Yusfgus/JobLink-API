using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Resumes.Queries.DownloadMyResume;

public record DownloadMyResumeQuery : IRequest<Result<Stream>>;
