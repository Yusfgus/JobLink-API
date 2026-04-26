using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies;
using MediatR;

namespace JobLink.Application.Features.Companies.Profile.Commands.RegisterCompany;

public class RegisterCompanyCommandHandler(IAppDbContext dbContext, IUserService userService, IJwtProvider jwtProvider)
    : IRequestHandler<RegisterCompanyCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(RegisterCompanyCommand request, CancellationToken ct)
    {
        Result<Guid> userIdResult = await userService.RegisterUser(request.User, UserRole.Company, ct);

        if (userIdResult.IsFailure)
        {
            return userIdResult.Errors;
        }

        Guid userId = userIdResult.Value!;

        Result<CompanyProfile> companyProfileResult = CompanyProfile.Create(userId, request.Name, request.Industry);

        if (companyProfileResult.IsFailure)
        {
            return companyProfileResult.Errors;
        }

        CompanyProfile companyProfile = companyProfileResult.Value!;

        await dbContext.CompanyProfiles.AddAsync(companyProfile, ct);

        var generateJWTRequest = new GenerateJWTRequest(userId, request.User.Email, UserRole.Company);

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