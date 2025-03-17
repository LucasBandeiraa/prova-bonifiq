using ProvaPub.Data;
using System.Threading.Tasks;

namespace ProvaPub.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestDbContext _ctx;

        public UnitOfWork(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}