using System.Collections.Generic;
using Telegram.Bot;
using TelegramBot.Bunker.Interfaces;
using TelegramBot.Commands;

namespace TelegramBot {
    public static class Bot
    {
        private static ITelegramBotClient _botClient;
        
        private static List<Command> _commandsList;

        public static readonly List<IBunkerGame> BunkerGames = new();
        
        public static IEnumerable<Command> Commands => _commandsList.AsReadOnly();
        
        public static ITelegramBotClient Get(string telegramBotKey)
        {
            if(_botClient != null)
            {
                return _botClient;
            }

            _commandsList = new List<Command>
            {
                new TestCommand(),
                new CreateBunkerCommand(),
                new JoinLobbyCommand(),
                new StartBunkerCommand()
            };
           
            _botClient = new TelegramBotClient(telegramBotKey);
            return _botClient;
        }
    }
}