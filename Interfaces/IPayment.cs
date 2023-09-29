using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IPayment
    {
        Payment Pay(decimal value);
    }
}