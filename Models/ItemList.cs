using ProvaPub.Interfaces;

namespace ProvaPub.Models
{
    public class ItemList<TEntity> : IEntityBase where TEntity : class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<TEntity>? Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}