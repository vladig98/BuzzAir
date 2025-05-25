namespace BuzzAir.Models.ViewModels
{
    public class PaginatedList<T>(IEnumerable<T> items, int count, int pageIndex, int pageSize) : List<T>(items)
    {
        public int PageIndex { get; private set; } = pageIndex;
        public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
