using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Card
    {
        public Card()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string,Effect> Effects { get; set; }

        public string ImagePath { get; set; }
    }
}
