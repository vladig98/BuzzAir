namespace BuzzAir.Models.ViewModels
{
    public class PaginatedList<T>(IEnumerable<T> items, long count, int pageIndex, int pageSize) : List<T>(items)
    {
        public int PageIndex { get; private set; } = pageIndex;
        public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageIndex > 0;
        // Subtract 1 since the pages are 0-based
        public bool HasNextPage => PageIndex < TotalPages - 1;
    }
}
