using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();
        //Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void Create(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        //Commands
        IEnumerable<Command> Get(int platformId);
        Command Get(int platformId, int commandId);
        void Create(int platformId, Command command);
    }
}