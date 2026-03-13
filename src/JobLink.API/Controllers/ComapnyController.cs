using JobLink.API.Contracts.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobLink.API.Mappings;
using JobLink.Application.Features.Companies.Queries.GetCompanyById;

namespace JobLink.API.Controllers;

[ApiController]
[Route("api/v1/companies")]
public class CompanyController(ISender sender) : ApiController
{
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
