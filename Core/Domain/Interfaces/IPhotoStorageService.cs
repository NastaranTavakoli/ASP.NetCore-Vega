using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Domain.Interfaces
{
    public interface IPhotoStorageService
    {
        Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}