using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bunker.Interfaces;

namespace TelegramBot.Commands
{
    public class JoinLobbyCommand : Command
    {
        public override string Name => "/join_lobby";
        
        private static IBunkerGame _bunkerGame;
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            _bunkerGame = Bot.BunkerGames.FirstOrDefault(x => x.GetGameId() == message.Text.Replace("/join_lobby ", ""));
            if (_bunkerGame is not null && GetLobbyDuplicatePlayers(message))
            {
                _bunkerGame.AddPlayer(message.From.Username);
                _bunkerGame.AddPlayerId(message.From.Id);
                var playersString = _bunkerGame.GetPlayers().Aggregate(string.Empty, (current, player) => current + " @" + player);
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id, 
                    text: "You successfully joined game: " + _bunkerGame.GetGameId() + "\n Active players: " + playersString
                );
            }
            else
            {
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id, 
                    text: "Your lobby id is invalid or you are already in lobby. \n If you want to leave the lobby use /exit_lobby" 
                );
            }
        }

        private bool GetLobbyDuplicatePlayers(Message message)
        {
            foreach (var bunkerGame in Bot.BunkerGames)
            {
                if (bunkerGame.GetPlayers().Any(player => player == message.From.Id.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public override bool Contains(string command)
        {
            return command.Contains(Name);
        }
    }
}