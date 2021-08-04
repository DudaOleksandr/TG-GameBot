using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class Program
    {
        private static ITelegramBotClient _botClient;
        private static IConfigurationRoot Configuration { get; set; }

        public static void Main()
        {
            _botClient = Bot.Get(GetTgKey());
            _botClient.OnMessage += Bot_OnMessage; 
            _botClient.StartReceiving();
            Console.ReadKey();
            _botClient.StopReceiving();
        }
        
        static void Bot_OnMessage(object sender, MessageEventArgs e) {
            var commands = Bot.Commands;
            if (e.Message.Text != null)
            {
                foreach(var command in commands)
                {
                    if (command.Contains(e.Message.Text))
                    {
                        command.Execute(e.Message, _botClient);
                        break;
                    }
                }
            }
        }

        private static string GetTgKey()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Users\oleksandr.duda\RiderProjects\TG-GameBot\TelegramBot\appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetSection("TelegramKey").Value;
        }
    }
}