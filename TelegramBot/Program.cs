using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class Program
    {
        private static ITelegramBotClient _botClient;

        public static void Main()
        {
            _botClient = Bot.Get();
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
    }
}