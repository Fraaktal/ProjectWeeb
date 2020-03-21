using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Card
    {
        public Card()
        {

        }

        public Card(int id, string name, Dictionary<string, Effect> effects, string path)
        {
            CardId = id;
            Name = name;
            Effects = effects;
            ImagePath = path;
        }

        public int Id { get; set; }

        public int CardId { get; set; }

        public string Name { get; set; }

        public Dictionary<string,Effect> Effects { get; set; }

        public string ImagePath { get; set; }
    }
}
