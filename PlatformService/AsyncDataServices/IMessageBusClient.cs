using PlatformService.DTOs;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishedNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}