using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class TestCommand : Command
    {
        protected virtual string Name => "/hello";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            
            await client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }

        public override bool Contains(string command)
        {
            return command == Name;
        }
    }
}