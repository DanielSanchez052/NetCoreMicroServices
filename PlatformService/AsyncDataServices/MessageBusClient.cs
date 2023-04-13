using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using PlatformService.DTOs;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection? _connection;
        private readonly IModel? _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory(){ 
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_Connectionshutdown;

                Console.WriteLine($"--> Connected To MessageBus");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"--> could not connect to the message Bus: {ex.Message}");
            }
        }

        public void PublishedNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if(_connection != null && _connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending Message...");
            }
            else{
                Console.WriteLine("--> RabbitMQ Connection Closed, not sending  ");
            }
        
        }

        private void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");

            if(_channel != null  && _channel.IsOpen)
            {
                _channel.Close();
                _connection?.Close();
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger"
                ,routingKey: ""
                ,basicProperties: null
                ,body: body);

            Console.WriteLine($"--> We Have sent {message}");
        }

        private void RabbitMQ_Connectionshutdown(object? sender, ShutdownEventArgs? e)
        {
            Console.WriteLine($"--> RabbitMQ Connection Shutdown");
        }
    }
}