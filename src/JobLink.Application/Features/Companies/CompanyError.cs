using JobLink.Domain.Common.Results;

namespace JobLink.Application.Features.Companies;

public static class CompanyError
{
    public static readonly Error NotFound = Error.NotFound("Company not found");
}