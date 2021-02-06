using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}