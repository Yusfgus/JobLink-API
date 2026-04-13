namespace JobLink.Application.Features.Identity.Commands.LogOut;

using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;

public sealed record LogOutCommand : IRequest<Result>;

public sealed class LogOutCommandHandler(IJwtProvider jwtProvider, IAppUser appUser) : IRequestHandler<LogOutCommand, Result>
{
    public async Task<Result> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        Guid? userId = appUser.UserId;
        if(userId is null)
        {
            return IdentityError.Unauthenticated;
        }

        var result = await jwtProvider.RevokeAllAsync(userId.Value, cancellationToken);
        if(result.IsFailure)
        {
            return result.Errors;
        }

        return Result.Success();
    }
}
