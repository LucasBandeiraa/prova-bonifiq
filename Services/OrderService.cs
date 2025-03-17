using ProvaPub.Data.Models;
using ProvaPub.Data;
using ProvaPub.Services.Payment;

public class OrderService
{
    private readonly TestDbContext _ctx;
    private readonly Dictionary<string, IPaymentMethod> _paymentMethods;

    public OrderService(TestDbContext ctx, IEnumerable<IPaymentMethod> paymentMethods)
    {
        _ctx = ctx;
        _paymentMethods = paymentMethods.ToDictionary(
            p => p.GetType().Name.Replace("Payment", ""),
            p => p,
            StringComparer.OrdinalIgnoreCase
        );
    }

    public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
    {
        if (_paymentMethods.TryGetValue(paymentMethod, out var payment))
        {
            var order = await payment.Pay(paymentValue, customerId);
            order.OrderDate = DateTime.UtcNow; //salva como UTC
            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();
            return order;
        }

        throw new ArgumentException("Método de pagamento inválido");
    }
}