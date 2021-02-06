using System;
using System.Threading.Tasks;
using Application.Controllers.Resources;
using AutoMapper;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace Application.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;

        }

        [HttpPost]
        public async Task<ActionResult<VehicleResource>> CreateVehicle([FromBody] SaveVehicleResource savevehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(savevehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SaveVehicleResource>> UpdateVehicle(int id, [FromBody] SaveVehicleResource savevehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            mapper.Map<SaveVehicleResource, Vehicle>(savevehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfWork.CompleteAsync();
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
            {
                return NotFound();
            }
            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResource>> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<ActionResult<QueryResultResource<VehicleResource>>> GetVehicles(VehicleQueryResource queryObj)
        {
            var queryObject = mapper.Map<VehicleQueryResource, VehicleQuery>(queryObj);
            var queryResult = await repository.GetVehicles(queryObject);
            var queryResultResource = mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);

            return Ok(queryResultResource);
        }
    }
}