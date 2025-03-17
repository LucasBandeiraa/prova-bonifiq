using Microsoft.EntityFrameworkCore;
using ProvaPub.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _ctx;

        public CustomerRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PagedList<Customer>> ListCustomers(int page, int pageSize)
        {
            var totalCount = await _ctx.Customers.CountAsync();
            var items = await _ctx.Customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var hasNext = (page * pageSize) < totalCount;

            return new PagedList<Customer>(items, totalCount, hasNext);
        }

        public async Task<Customer?> FindAsync(int customerId)
        {
            return await _ctx.Customers.FindAsync(customerId);
        }

        public async Task<int> CountAsync(int customerId)
        {
            return await _ctx.Customers.CountAsync(c => c.Id == customerId && c.Orders.Any());
        }
    }
}