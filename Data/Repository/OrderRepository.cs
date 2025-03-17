using Microsoft.EntityFrameworkCore;
using ProvaPub.Data.Repository;
using ProvaPub.Data;
using System;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestDbContext _ctx;

        public OrderRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> CountAsync(int customerId, DateTime baseDate)
        {
            return await _ctx.Orders.CountAsync(o => o.CustomerId == customerId && o.OrderDate >= baseDate);
        }
    }
}