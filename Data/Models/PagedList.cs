namespace ProvaPub.Data.Models
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }

        public PagedList(List<T> items, int totalCount, bool hasNext)
        {
            Items = items;
            TotalCount = totalCount;
            HasNext = hasNext;
        }
    }
}