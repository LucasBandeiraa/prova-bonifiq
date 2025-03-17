using ProvaPub.Data.Models;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public abstract class BaseService<T>
    {
        protected readonly int PageSize = 10;

        public abstract Task<PagedList<T>> List(int page);
    }
}