using System.Collections.Generic;
using Telegram.Bot;
using TelegramBot.Bunker;
using TelegramBot.Bunker.Interfaces;
using TelegramBot.Commands;

namespace TelegramBot {
    public static class Bot {
        
        private static ITelegramBotClient _botClient;
        
        private static List<Command> _commandsList;

        public static readonly List<IBunkerGame> BunkerGames = new();
        
        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();
        
        public static  ITelegramBotClient Get()
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

            _botClient = new TelegramBotClient(AppSettings.Key);
            return _botClient;
        }
    }
}