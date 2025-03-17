using Microsoft.EntityFrameworkCore;
using ProvaPub.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _ctx;

        public ProductRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PagedList<Product>> ListProducts(int page, int pageSize)
        {
            var totalCount = await _ctx.Products.CountAsync();
            var items = await _ctx.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var hasNext = (page * pageSize) < totalCount;

            return new PagedList<Product>(items, totalCount, hasNext);
        }
    }
}