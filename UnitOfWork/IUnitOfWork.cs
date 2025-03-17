using System.Threading.Tasks;

namespace ProvaPub.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}