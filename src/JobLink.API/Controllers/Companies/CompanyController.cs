using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.Application.Features.Companies.Profile.Queries.GetMyCompany;
using JobLink.Application.Features.Companies.Profile.Queries.GetCompanyById;
using JobLink.API.Contracts.Companies;

namespace JobLink.API.Controllers.Companies;

[ApiController]
[Route("api/v1/companies")]
// [Authorize(Roles = nameof(UserRole.Company))]
public class CompanyController(ISender sender) : ApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompanyById([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetCompanyByIdQuery(id), ct);

        return result.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyCompany(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyCompanyQuery(), ct);

        return result.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateCompany(UpdateCompanyRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            NoContent,
            errors => Problem(errors)
        );
    }
}
