using Microsoft.EntityFrameworkCore;
using ProvaPub.Data.Models;
using System.Threading.Tasks;

namespace ProvaPub.Data.Repository
{
    public class RandomNumberRepository : IRandomNumberRepository
    {
        private readonly TestDbContext _ctx;

        public RandomNumberRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Exists(int number)
        {
            return await _ctx.Numbers.AnyAsync(n => n.Number == number);
        }

        public async Task Add(RandomNumber randomNumber)
        {
            await _ctx.Numbers.AddAsync(randomNumber);
        }
    }
}