using ProvaPub.Data.Models;
using ProvaPub.Data.Repository;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class CustomerService : BaseService<Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public override async Task<PagedList<Customer>> List(int page)
        {
            return await _customerRepository.ListCustomers(page, PageSize);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            // Business Rule: Non-registered customers cannot purchase
            var customer = await _customerRepository.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exist");

            // Business Rule: A customer can purchase only once per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _orderRepository.CountAsync(customerId, baseDate);
            if (ordersInThisMonth > 0)
                return false;

            // Business Rule: A customer that never bought before can make a first purchase of maximum 100.00
            var haveBoughtBefore = await _customerRepository.CountAsync(customerId);
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            // Business Rule: A customer can purchase only during business hours and working days
            if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
                return false;

            return true;
        }
    }
}