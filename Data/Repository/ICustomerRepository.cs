using ProvaPub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public interface ICustomerRepository
    {
        Task<PagedList<Customer>> ListCustomers(int page, int pageSize);
        Task<Customer?> FindAsync(int customerId);
        Task<int> CountAsync(int customerId);
    }
}