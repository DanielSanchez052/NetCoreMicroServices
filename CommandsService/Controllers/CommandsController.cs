using Microsoft.AspNetCore.Mvc;
using CommandsService.Data;
using CommandsService.Dtos;
using AutoMapper;
using CommandsService.Models;

namespace CommandsService.Controllers
{
    [Route("api/command/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetcommandsForPlatform(int platformId)
        {
            Console.WriteLine($"---> hitCommandsForPlatform: {platformId}");

            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _repo.Get(platformId); 

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"---> hitCommandForPlatform: {platformId} / {commandId}");

            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _repo.Get(platformId, commandId); 

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(command));   
        }
    
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, [FromBody] CommandCreateDto commandDto)
        {
            Console.WriteLine($"---> Hit CreateCommandForPlatform: {platformId}");
            
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repo.Create(platformId,command);
            _repo.SaveChanges();

            var commandRead = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), 
            new {platformId = platformId, commandId = commandRead.Id}
            , commandRead);
        }
    }
}