using Microsoft.EntityFrameworkCore;

namespace JobLink.Application.Common.Models;

public sealed class PaginatedList<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;

    public IReadOnlyCollection<T>? Items { get; init; }

    public PaginatedList(int pageNumber, int pageSize, int totalCount, IReadOnlyCollection<T>? items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }
}

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, int totalCount, CancellationToken cancellationToken)
    {
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(pageNumber, pageSize, totalCount, items);
    }
}