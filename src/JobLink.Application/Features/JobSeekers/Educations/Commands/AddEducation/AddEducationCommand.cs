using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.AddEducation;

public sealed record AddEducationCommand(
    string Degree,
    string Country,
    string Institution,
    string FieldOfStudy,
    DateOnly StartDate,
    DateOnly EndDate,
    AcademicGrade Grade
) : IRequest<Result<Guid>>;
