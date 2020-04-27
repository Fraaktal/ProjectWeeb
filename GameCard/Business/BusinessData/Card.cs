using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Card
    {
        public enum Status
        {
            InHand,InDeck,Waiting,Charging,Disabled,Sleeping // A compléter au fur et a mesure
        }

        public Card()
        {

        }

        public Card(int id, string name, HashSet<Effect> effects, string path)
        {
            CardId = id;
            Name = name;
            Effects = effects;
            ImagePath = path;
            CurrentStatus = Status.InDeck;
        }

        public int Id { get; set; }

        public int CardId { get; set; }

        public HashSet<Effect> Effects { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public Status CurrentStatus { get; set; }
    }
}
