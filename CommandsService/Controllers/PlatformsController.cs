
using Microsoft.AspNetCore.Mvc;
using CommandsService.Data;
using AutoMapper;
using CommandsService.Dtos;

namespace CommandsService.Controllers
{
    [Route("api/command/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepository _repo;
        private readonly IMapper _mapper;
        public PlatformsController(ICommandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Geting Platforms from CommandsService");
            var platforms = _repo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");


            return Ok("Inbound test of from Platforms Controller");
        }
    }
}