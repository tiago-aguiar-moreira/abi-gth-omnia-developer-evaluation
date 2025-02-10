using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Common.Helpers;

public class PaginatedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalItems { get; private set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalItems = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
    {
        if (pageNumber.GetValueOrDefault() < 1)
            pageNumber = 1;

        if (pageSize.GetValueOrDefault() < 10)
            pageSize = 10;

        var count = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((pageNumber.GetValueOrDefault() - 1) * pageSize.GetValueOrDefault())
            .Take(pageSize.GetValueOrDefault()).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, pageNumber.GetValueOrDefault(), pageSize.GetValueOrDefault());
    }
}