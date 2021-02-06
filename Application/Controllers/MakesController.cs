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

    public class MakesController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public MakesController(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }



        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

    }
}