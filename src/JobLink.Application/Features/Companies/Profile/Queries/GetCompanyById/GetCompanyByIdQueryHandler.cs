using JobLink.Application.Common.DTOs;
using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Companies.DTOs;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Profile.Queries.GetCompanyById;

public class GetCompanyByIdQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetCompanyByIdQuery, Result<CompanyProfileDto>>
{
    public async Task<Result<CompanyProfileDto>> Handle(GetCompanyByIdQuery request, CancellationToken ct)
    {
        CompanyProfileDto? companyDto = await dbContext.CompanyProfiles.AsNoTracking()
            .Where(x => x.Id == request.Id)
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