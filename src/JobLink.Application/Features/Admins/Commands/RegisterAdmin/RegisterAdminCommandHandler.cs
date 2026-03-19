using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Admins.Commands.RegisterAdmin;

public class RegisterAdminCommandHandler(IAppDbContext dbContext, IUserService userService, IJwtProvider jwtProvider)
    : IRequestHandler<RegisterAdminCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(RegisterAdminCommand request, CancellationToken ct)
    {
        Result<Guid> userIdResult = await userService.RegisterUser(request.User, UserRole.Admin, ct);

        if (userIdResult.IsFailure)
        {
            return userIdResult.Errors;
        }

        Guid userId = userIdResult.Value!;

        var generateJWTRequest = new GenerateJWTRequest(userId, request.User.Email, UserRole.Admin);

        Result<TokenDto> tokenResult = await jwtProvider.GenerateJWTAsync(generateJWTRequest, ct);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        TokenDto token = tokenResult.Value!;

        await dbContext.SaveChangesAsync(ct);

        return token;
    }
}