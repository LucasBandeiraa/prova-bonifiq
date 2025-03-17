using System;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public interface IOrderRepository
    {
        Task<int> CountAsync(int customerId, DateTime baseDate);
    }
}