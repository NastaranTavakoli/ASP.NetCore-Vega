using System.Threading.Tasks;
using Core.Domain;
using Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Core.Domain.Interfaces;

namespace Infrastructure
{

    public class PhotoService : IPhotoService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorageService photoStorageService;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorageService photoStorageService)
        {
            this.photoStorageService = photoStorageService;
            this.unitOfWork = unitOfWork;

        }
        public async Task<Photo> UploadPhoto(string uploadsFolderPath, IFormFile file, Vehicle vehicle)
        {
            var fileName = await photoStorageService.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo() { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}