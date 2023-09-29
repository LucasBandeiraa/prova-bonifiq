namespace ProvaPub.Models
{
    public class ItemList<IEntityBase>
    {
        public virtual List<IEntityBase>? Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }

    }
}