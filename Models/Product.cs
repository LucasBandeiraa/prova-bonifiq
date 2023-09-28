using ProvaPub.Interfaces;

namespace ProvaPub.Models
{
	public class Product : IEntityBase
    {
		public int Id { get; set; }	

		public string Name { get; set; }
	}
}
