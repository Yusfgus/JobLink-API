using JobLink.Domain.Common.Results;
using MediatR;
using JobLink.Domain.Common.Enums;
using JobLink.Application.Common.DTOs;

namespace JobLink.Application.Features.JobSeekers.Commands.RegisterJobSeeker;

public sealed record RegisterJobSeekerCommand(
    RegisterUserDto User,
    string FirstName,
    string LastName,
    Gender Gender
) : IRequest<Result<TokenDto>>;
