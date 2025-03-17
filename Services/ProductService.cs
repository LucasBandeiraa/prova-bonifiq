using ProvaPub.Data.Models;
using ProvaPub.Data.Repository;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class ProductService : BaseService<Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<PagedList<Product>> List(int page)
        {
            return await _productRepository.ListProducts(page, PageSize);
        }
    }
}