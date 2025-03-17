using ProvaPub.Data.Models;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public interface IRandomNumberRepository
    {
        Task<bool> Exists(int number);
        Task Add(RandomNumber randomNumber);
    }
}