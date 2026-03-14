using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Companies;
using MediatR;

namespace JobLink.Application.Features.Companies.Commands.RegisterCompany;

public class RegisterCompanyHandler(IAppDbContext dbContext, IUserService userService, IJwtProvider jwtProvider)
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

        Result<CompanyProfile> companyProfileResult = CompanyProfile.Register(userId, request.Name, request.Industry);

        if (companyProfileResult.IsFailure)
        {
            return companyProfileResult.Errors;
        }

        CompanyProfile companyProfile = companyProfileResult.Value!;

        // List<Result<Address>> addressResults = request.Locations.ConvertAll(address => Address.Create(address.Country, address.City, address.Area));

        // if (addressResults.Any(x => x.IsFailure))
        // {
        //     return addressResults.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        // }

        // List<Address> locations = addressResults.ConvertAll(x => x.Value!);

        // Result addLocationsResult = companyProfile.AddLocations(locations);

        // if (addLocationsResult.IsFailure)
        // {
        //     return addLocationsResult.Errors;
        // }

        await dbContext.CompanyProfiles.AddAsync(companyProfile, ct);

        Result<TokenDto> tokenResult = await jwtProvider.GenerateJWTAsync(userId, request.User.Email, UserRole.Company, ct);

        if (tokenResult.IsFailure)
        {
            return tokenResult.Errors;
        }

        TokenDto token = tokenResult.Value!;

        await dbContext.SaveChangesAsync(ct);

        return token;
    }
}