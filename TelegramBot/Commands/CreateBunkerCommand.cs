using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bunker;
using TelegramBot.Bunker.Interfaces;

namespace TelegramBot.Commands
{
    public class CreateBunkerCommand : Command
    {
        protected virtual string Name => "/create_bunker";
        
        private static IBunkerGame _bunkerGame;
        
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            _bunkerGame = new BunkerGame(message.From.Id);
            Bot.BunkerGames.Add(_bunkerGame);
            _bunkerGame.AddPlayer(message.From.Username);
            _bunkerGame.AddPlayerId(message.From.Id);
            var playersString = _bunkerGame.GetPlayers().Aggregate(string.Empty, (current, player) => current + " @" + player);
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id, 
                text: "You successfully created game: " + _bunkerGame.GetGameId() + "\n Active players: " + playersString
                );
        }

        public override bool Contains(string command)
        {
            return command.Contains(Name);
        }
    }
}