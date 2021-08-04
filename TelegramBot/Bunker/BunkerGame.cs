using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TelegramBot.Bunker.Enums;
using TelegramBot.Bunker.Interfaces;
using TelegramBot.Bunker.Models;

namespace TelegramBot.Bunker
{
    public class BunkerGame : IBunkerGame
    {
        private readonly string _gameId;
        private readonly long _gameOwner;

        private readonly List<string> _bunkerPlayers = new();
        private List<string> _bunkerHistories = new();
        private readonly List<long> _bunkerPlayersId = new();

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

        private string GetBunkerHistory(string path, Random r)
        {
            if (_bunkerHistories.Count < 1)
            {
                using var sr = new StreamReader(path);
                while (sr.Peek() >= 0) 
                {
                    _bunkerHistories.AddRange(sr.ReadToEnd().Split("/////"));
                }
            }
        
            var indexToChoose = r.Next(_bunkerHistories.Count);
            var elementToReturn = _bunkerHistories[indexToChoose];
            _bunkerHistories.Remove(elementToReturn);

            return elementToReturn;
        }

        public string GetPlayerCard(long playerId)
        {
            var r = new Random();
            return new BunkerCard
                ((int)playerId,
                    r.Next(16,80),
                    RandomEnumValue<Sex>(r),
                    RandomEnumValue<Professions>(r),
                    RandomEnumValue<BodyComposition>(r),
                    RandomEnumValue<ChildRelation>(r),
                    RandomEnumValue<Health>(r),
                    GetBunkerHistory(@"C:\Users\oleksandr.duda\RiderProjects\TG-GameBot\TelegramBot\BunkerHistory.txt", r)
                    
                ).ToString();
        }

        private static string RandomEnumValue<T> (Random r)
        {
            var v = Enum.GetValues (typeof (T));
            return  v.GetValue (r.Next(v.Length))?.ToString();
        }
    }
}