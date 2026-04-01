namespace app.Api.ViewModels;

public class PagedResultViewModel<T>
{
    public IEnumerable<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }

    public PagedResultViewModel(
        IEnumerable<T> items,
        int page,
        int pageSize,
        int total)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        Total = total;
    }
}