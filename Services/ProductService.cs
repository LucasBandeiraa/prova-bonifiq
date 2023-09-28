using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : BaseService<Product>
    {
        private readonly TestDbContext _ctx;

        public ProductService(TestDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

    }
}