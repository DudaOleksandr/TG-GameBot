using System;
using System.Collections.Generic;
using System.Linq;
using TelegramBot.Bunker.Interfaces;

namespace TelegramBot.Bunker
{
    public class BunkerGame : IBunkerGame
    {
        private readonly string _gameId;
        private readonly long _gameOwner;

        private List<string> _bunkerPlayers = new();
        private List<long> _bunkerPlayersId = new();

        public BunkerGame(long gameOwner)
        {
            _gameOwner = gameOwner;
            _gameId = Guid.NewGuid().ToString();
        }

        public string GetGameId()
        {
            return _gameId;
        }

        public void AddPlayer(string chatId)
        {
            if (!_bunkerPlayers.Contains(chatId))
                _bunkerPlayers.Add(chatId);
        }
        
        public void AddPlayerId(long playerId)
        {
            if (!_bunkerPlayersId.Contains(playerId))
                _bunkerPlayersId.Add(playerId);
        }
        
        public List<string> GetPlayers()
        {
            return _bunkerPlayers;
        }
        
        public List<long> GetPlayersId()
        {
            return _bunkerPlayersId;
        }
        
        public long GetOwner()
        {
            return _gameOwner;
        }

        public string GetPlayerCard(long playerId)
        {
            return playerId.ToString();
        }
    }
}