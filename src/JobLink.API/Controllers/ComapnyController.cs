using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.Application.Features.Companies.Queries.GetMyCompany;
using Microsoft.AspNetCore.Authorization;
using JobLink.Domain.Common.Enums;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/companies")]
[Authorize(Roles = nameof(UserRole.Company))]
public class CompanyController(ISender sender) : ApiController
{
    [HttpGet("me")]
    public async Task<IActionResult> GetCompany(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyCompanyQuery(), ct);

        return result.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
    }
}
