namespace JobLink.API.Contracts;

public sealed record PageRequest(int Page = 1, int PageSize = 10);