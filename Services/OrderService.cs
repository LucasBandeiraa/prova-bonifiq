using ProvaPub.Interfaces;
using ProvaPub.Models;

public enum PaymentMethod
{
    PIX,
    CREDIT_CARD,
    PAYPAL
}

namespace ProvaPub.Services
{

    public class OrderService
    {

        private readonly IPayment _payment;

        public OrderService(IPayment payment)
        {
            _payment = payment;
        }

        public async Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
        {
            var _orders = new Dictionary<PaymentMethod, Delegate>();
            _orders[paymentMethod] = new Func<IPayment, decimal, Payment>(Func1);

            var paymentResult =  _orders[paymentMethod].DynamicInvoke(_payment, paymentValue);

            if (paymentResult == null)
            {
                throw new Exception();
            }

            //Payment paymentResult = Convert.ChangeType(res, Payment);

            return await Task.FromResult(new Order()
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Value = paymentValue,
                Payment = (Payment) paymentResult
            });

        }

        public static Payment Func1(IPayment payment, decimal paymentValue)
        {
            return payment.Pay(paymentValue);
        }
    }
}
