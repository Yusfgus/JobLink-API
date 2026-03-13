using JobLink.Application.Common.Interfaces;
using JobLink.Domain.Common.Enums;
using JobLink.Domain.Common.Results;
using JobLink.Domain.Common.ValueObjects;
using JobLink.Domain.Companies;
using MediatR;

namespace JobLink.Application.Features.Companies.Commands.RegisterCompany;

public class RegisterCompanyHandler(IAppDbContext dbContext, IUserService userService) : IRequestHandler<RegisterCompanyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterCompanyCommand request, CancellationToken ct)
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

        await dbContext.SaveChangesAsync(ct);

        return companyProfile.Id;
    }
}