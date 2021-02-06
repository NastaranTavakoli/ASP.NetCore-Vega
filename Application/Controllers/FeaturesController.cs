using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Controllers.Resources;
using AutoMapper;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Controllers
{

    public class FeaturesController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public FeaturesController(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }



        [HttpGet("/api/features")]
        public async Task<IEnumerable<IdNameResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<IdNameResource>>(features);
        }
    }
}