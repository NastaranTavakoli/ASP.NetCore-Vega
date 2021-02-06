using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Domain.Interfaces
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(string uploadsFolderPath, IFormFile file, Vehicle vehicle);
    }
}