namespace TelegramBot.Bunker.Models
{
    public class BunkerCard
    {
        
        private readonly int _playerId;

        private readonly int _age;

        private readonly string _sex;
        
        private readonly string _profession;

        private readonly string _bodyComposition;
        
        private readonly string _childRelation;

        private readonly string _health;
        
        private readonly string _bunkerHistory;

        public BunkerCard(int playerId, int age, string sex, string profession, string bodyComposition, string childRelation, string health, string bunkerHistory)
        {
            _playerId = playerId;
            _age = age;
            _sex = sex;
            _profession = profession;
            _bodyComposition = bodyComposition;
            _childRelation = childRelation;
            _health = health;
            _bunkerHistory = bunkerHistory;
        }

        public override string ToString()
        {
            return  _bunkerHistory
                             + "\nPerson: " + _playerId 
                             + "\n You are: \n " + _sex + ", " + _age + " years | " + _childRelation 
                             + "\n " + _profession 
                             + "\n Your body composition is: " + _bodyComposition 
                             + "\n Your health: " + _health;
        }
    }
}