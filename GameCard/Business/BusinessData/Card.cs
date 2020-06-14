using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Card
    {
        public enum Status
        {
            InHand, InDeck, Waiting, Charging, Disabled, Sleeping ,Confuse
        }

        public Card()
        {

        }

        public Card(int id, string name,int life, int strength, HashSet<Effect> effects)
        {
            CardId = id;
            Name = name;
            Effects = effects;
            CurrentStatus = Status.InDeck;
            Strength = strength;
            Life = life;
            Shield = 0;
        }
        public int CardId { get; set; }

        public HashSet<Effect> Effects { get; set; }

        public string Name { get; set; }

        public Status CurrentStatus { get; set; }

        public int Life { get; set; }

        public int Shield { get; set; }

        public int Strength { get; set; }
    }
}
