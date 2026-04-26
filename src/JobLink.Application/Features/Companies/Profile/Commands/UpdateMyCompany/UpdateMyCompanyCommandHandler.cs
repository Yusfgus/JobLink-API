using JobLink.Application.Common.Interfaces;
using JobLink.Application.Features.Identity;
using JobLink.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Features.Companies.Profile.Commands.UpdateMyCompany;

internal sealed class UpdateMyCompanyCommandHandler(IAppDbContext dbContext, IAppUser appUser) : IRequestHandler<UpdateMyCompanyCommand, Result>
{
    public async Task<Result> Handle(UpdateMyCompanyCommand request, CancellationToken cancellationToken)
    {
        Guid? user = appUser.UserId;
        if (user is null)
        {
            return IdentityError.Unauthenticated;
        }

        var companyProfile = await dbContext.CompanyProfiles.FirstOrDefaultAsync(x => x.UserId == user, cancellationToken);
        if (companyProfile is null)
        {
            return CompanyError.NotFound;
        }

        var result = companyProfile.Update(
            name: request.Name,
            industry: request.Industry,
            description: request.Description,
            logoUrl: request.LogoUrl,
            website: request.Website
        );

        if (result.IsFailure)
        {
            return result;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}