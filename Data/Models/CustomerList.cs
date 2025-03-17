namespace ProvaPub.Data.Models
{
    public class CustomerList
    {
        public List<CustomerList>? Customers { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}


