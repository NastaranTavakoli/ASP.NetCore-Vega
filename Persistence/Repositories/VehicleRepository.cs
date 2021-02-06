using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Repositories
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext context;
        public VehicleRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Vehicles.FindAsync(id);
            }
            return await context.Vehicles.Include(v => v.Features).ThenInclude(vf => vf.Feature).Include(v => v.Model).ThenInclude(m => m.Make).SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            var query = context.Vehicles.Include(v => v.Model).ThenInclude(m => m.Make).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Func<Vehicle, object>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}