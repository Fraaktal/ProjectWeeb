using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Deck
    {
        public Deck()
        {

        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}
