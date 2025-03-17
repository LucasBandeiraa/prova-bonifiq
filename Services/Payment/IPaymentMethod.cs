using ProvaPub.Data.Models;

namespace ProvaPub.Services.Payment
{
    public interface IPaymentMethod
    {
        Task<Order> Pay(decimal paymentValue, int customerId);
    }
}
