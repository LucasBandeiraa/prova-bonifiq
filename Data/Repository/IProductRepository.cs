using ProvaPub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> ListProducts(int page, int pageSize);
    }
}