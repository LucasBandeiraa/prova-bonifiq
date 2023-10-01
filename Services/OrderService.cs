using Microsoft.SqlServer.Server;
using ProvaPub.Interfaces;
using ProvaPub.Models;


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
            var paymentResult = ToPaymentMethod(paymentMethod, _payment, paymentValue);

            if (paymentResult == null)
            {
                throw new Exception();
            }

            return await Task.FromResult(new Order()
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Value = paymentValue,
                Payment = paymentResult
            });
        }

        public static Payment ToPaymentMethod(PaymentMethod paymentMethod, IPayment payment, decimal value) => paymentMethod switch
        {
            PaymentMethod.PIX => PaymentPix(payment, value),
            PaymentMethod.CREDIT_CARD => PaymentCreditCard(payment, value),
            PaymentMethod.PAYPAL => PaymentPayPal(payment, value),
            _ => throw new ArgumentOutOfRangeException(nameof(paymentMethod), $"Not expected paymentMethod value: {paymentMethod}"),
        };

        public static Payment PaymentPix(IPayment payment, decimal paymentValue)
        {
            return payment.Pay(paymentValue);
        }

        public static Payment PaymentCreditCard(IPayment payment, decimal paymentValue)
        {
            return payment.Pay(paymentValue);
        }

        public static Payment PaymentPayPal(IPayment payment, decimal paymentValue)
        {
            return payment.Pay(paymentValue);
        }
    }

    public enum PaymentMethod
    {
        PIX,
        CREDIT_CARD,
        PAYPAL
    }
}
