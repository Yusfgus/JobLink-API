using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.JobSeekers.Educations.Commands.UpdateEducation;

public sealed record UpdateEducationCommand(
    Guid Id,
    string? Degree,
    string? Country,
    string? Institution,
    string? FieldOfStudy,
    DateOnly? StartDate,
    DateOnly? EndDate,
    AcademicGrade? Grade
) : IRequest<Result>;
