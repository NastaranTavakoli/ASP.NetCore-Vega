using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;

namespace Persistence.Repositories
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery vehicleQuery);
    }
}