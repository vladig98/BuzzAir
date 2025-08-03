namespace BuzzAir.Utilities;

/// <summary>
/// A simple paged subset of a larger dataset.
/// </summary>
public sealed class PaginatedList<T> : List<T>
{
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Creates a new <see cref="PaginatedList{T}"/>. Only public ctor.
    /// </summary>
    public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        if (count > 0)
        {
            pageIndex = Math.Clamp(pageIndex, 1, count);
        }

        pageSize = Math.Clamp(pageSize, 10, 100);

        TotalCount = count;
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }
}
