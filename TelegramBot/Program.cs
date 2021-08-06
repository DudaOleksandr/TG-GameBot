using System;
using Microsoft.Extensions.Configuration;
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
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return Configuration.GetSection("TelegramKey").Value;
        }
    }
}