using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bunker.Interfaces;

namespace TelegramBot.Commands
{
    public class StartBunkerCommand : Command
    {
        protected virtual string Name => "/start_bunker";
        
        private static IBunkerGame _bunkerGame;
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            _bunkerGame = Bot.BunkerGames.FirstOrDefault(x => x.GetOwner() == message.From.Id);
            if (_bunkerGame is null)
            {
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id, 
                    text: "You cannot start a game, because you are not a creator, ask him to start the game" 
                );
            }
            else
            {
                foreach (var player in _bunkerGame.GetPlayersId())
                {
                    var playerCard =  _bunkerGame.GetPlayerCard(player);
                    await client.SendTextMessageAsync(
                        chatId: player, 
                        text: playerCard
                    );
                }
            }
        }

        public override bool Contains(string command)
        {
            return command.Contains(Name);
        }
    }
}