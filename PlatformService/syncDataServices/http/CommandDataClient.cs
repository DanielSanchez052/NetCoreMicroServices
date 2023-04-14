using System.Text;
using System.Text.Json;
using PlatformService.DTOs;

namespace PlatformService.syncDataServices.http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CommandDataClient(HttpClient httpClient, IConfiguration confifiguration)
        {
            _httpClient = httpClient;
            _configuration = confifiguration;
        
            // _httpClient.
        }

        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                    $"{_configuration["Services:CommandService:Host"]}{_configuration["Services:CommandService:Platforms"]}"
                    ,httpContent
                );
        
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("-->Sync POST To CommandSevice NOT OK!");
            }
            Console.WriteLine("-->Sync POST To CommandSevice OK!");
        }
    }
}