using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext Context;

        public PhotoRepository(DataContext context)
        {
            this.Context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await Context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}