using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Results;
using MediatR;

namespace JobLink.Application.Features.Identity.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IJwtProvider jwtProvider) : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await jwtProvider.RefreshAsync(request.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        return result.Value!;
    }
}
