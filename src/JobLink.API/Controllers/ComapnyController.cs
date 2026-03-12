using JobLink.API.Contracts.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;
using JobLink.Application.Features.Companies.Queries.GetCompanyById;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCompanyRequest request, CancellationToken ct)
    {
        var result = await sender.Send(request.ToCommand(), ct);

        return result.Match(
            id => CreatedAtAction(nameof(GetCompany), new { id }, id),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetCompanyByIdQuery(id), ct);

        return result.Match(
            company => Ok(company),
            errors => Problem(errors)
        );
    }
}
