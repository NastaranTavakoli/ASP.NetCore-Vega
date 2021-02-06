using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}