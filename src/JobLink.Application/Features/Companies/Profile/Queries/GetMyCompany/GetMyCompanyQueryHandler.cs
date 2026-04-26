using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Profile.Queries.GetMyCompany;

public class GetMyCompanyQueryHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<GetMyCompanyQuery, Result<CompanyProfileDto>>
{
    public async Task<Result<CompanyProfileDto>> Handle(GetMyCompanyQuery request, CancellationToken ct)
    {
        Guid? userId = appUser.UserId;

        if (userId is null)
        {
            // do something
            return IdentityError.Unauthenticated;
        }

        CompanyProfileDto? companyDto = await dbContext.CompanyProfiles.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new CompanyProfileDto(
                x.Id.ToString(),
                x.Name,
                x.User!.Email,
                x.Website,
                x.Description,
                x.LogoUrl,
                x.CompanyLocations.Select(y => new AddressDto(
                    y.Address.Country,
                    y.Address.City,
                    y.Address.Area
                )).ToList()
            ))
            .FirstOrDefaultAsync(ct);

        if (companyDto is null)
        {
            // do something
            return CompanyError.NotFound;
        }

        return companyDto;
    }
}