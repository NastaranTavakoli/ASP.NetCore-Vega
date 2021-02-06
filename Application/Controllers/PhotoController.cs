using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Controllers.Resources;
using Application.Types;
using AutoMapper;
using Core.Domain;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Repositories;

namespace Application.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IVehicleRepository VehicleRepo;
        private readonly IWebHostEnvironment host;
        private readonly PhotoSettings photosettings;
        private readonly IMapper mapper;

        private readonly IPhotoRepository photoRepo;
        private readonly IPhotoService photoService;

        public PhotoController(IVehicleRepository vehicleRepo, IPhotoRepository photoRepo, IWebHostEnvironment host, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoService photoService)
        {
            this.photoService = photoService;
            this.photoRepo = photoRepo;
            this.photosettings = options.Value;
            this.mapper = mapper;
            this.host = host;
            this.VehicleRepo = vehicleRepo;
        }


        [HttpPost]
        public async Task<ActionResult<PhotoResource>> Upload(int vehicleId, [FromForm] IFormFile file)
        {
            var vehicle = await VehicleRepo.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
            {
                return NotFound();
            }

            if (file == null)
            {
                return BadRequest("Null File");
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty File");
            }
            if (file.Length > photosettings.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }
            if (!photosettings.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }

            if (string.IsNullOrWhiteSpace(host.WebRootPath))
            {
                host.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var uploadsFolderPath = Path.Combine(host.WebRootPath, "Uploads");
            var photo = await photoService.UploadPhoto(uploadsFolderPath, file, vehicle);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoResource>>> GetPhotos(int vehicleId)
        {

            var photos = await photoRepo.GetPhotos(vehicleId);
            return Ok(mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos));

        }
    }
}