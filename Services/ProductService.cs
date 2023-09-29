using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using static ProvaPub.Models.Payment;

namespace ProvaPub.Services
{
    public class PaymentService : IPayment
    {
        public Payment Pay(decimal value)
        {
            return new Payment()
            {
                Id = "1",
                Quantity = value,
                TransactionDate = DateTime.Now,
                Status = PaymentStatus.Success,
            };
        }
    }
}