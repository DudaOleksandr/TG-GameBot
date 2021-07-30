using System.Collections.Generic;

namespace TelegramBot.Bunker.Interfaces
{
    public interface IBunkerGame
    {
        public string GetGameId();

        public void AddPlayer(string chatId);

        public void AddPlayerId(long playerId);
        
        public List<string> GetPlayers();

        public List<long> GetPlayersId();

        public long GetOwner();

        public string GetPlayerCard(long playerId);

    }
}