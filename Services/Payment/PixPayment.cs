using ProvaPub.Data.Models;
using ProvaPub.Services.Payment;

public class PixPayment : IPaymentMethod
{
    public async Task<Order> Pay(decimal paymentValue, int customerId)
    {
        var order = new Order
        {
            Value = paymentValue,
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow // Salva como UTC
        };

        await Task.CompletedTask;
        return order;
    }
}